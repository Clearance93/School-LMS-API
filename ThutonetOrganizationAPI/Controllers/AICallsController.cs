using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrganizationDTO;
using OrganizationDTO.Dto;
using OrganizationDTO.Dto.AIDto;
using OrganizationIInterface.IService.Assignments;
using OrganizationModels.Model;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

[ApiController]
[Route("api/[controller]")]
public class AIAssistanceController : ControllerBase
{
    private readonly IAssignmentServiceInterface _Assignment;
    private readonly IHttpClientFactory _Client;
    private readonly IConfiguration _Configuration;
    private readonly string _GroqUrl;
    private readonly string _GroqApiKey;
    private readonly IMapper _Mapper;

    public AIAssistanceController(IAssignmentServiceInterface assignment,
                                  IHttpClientFactory client,
                                  IConfiguration configuration,
                                  IMapper mapper)
    {
        _Assignment = assignment ?? throw new ArgumentNullException(nameof(assignment));
        _Client = client;
        _Configuration = configuration;
        _GroqUrl = configuration["Groq:GroqUrl"];
        _GroqApiKey = configuration["Groq:GroqAPIKey"];
        _Mapper = mapper;
    }

    [HttpPost("aiGradeAssistance")]
    public async Task<IActionResult> GettingAllRequestValues(Guid assignmentId, Guid studentId)
    {
        try
        {
            var request = await _Assignment.GettingAllMarkingRequestByAssignmentId(assignmentId, studentId);

            if (request == null || string.IsNullOrWhiteSpace(request.StudentAnswers))
            {
                return BadRequest("Student answer is required.");
            }

            string prompt;

            if (!string.IsNullOrWhiteSpace(request.TeacherRubric))
            {
                prompt = $@"
                    You are a professional teacher grading a student's answer.
                    
                    Use the following rubric strictly:
                    {request.TeacherRubric}
                    
                    Student Answer:
                    {request.StudentAnswers}
                    
                    Return ONLY valid JSON in this exact structure with no extra text:
                    
                    {{
                      ""grammar"": number (0-100),
                      ""clarity"": number (0-100),
                      ""content"": number (0-100),
                      ""totalMarks"": number (0-100),
                      ""feedback"": ""Constructive feedback"",
                      ""strength"": ""Strongest point"",
                      ""weakness"": ""Weakest point"",
                      ""improvement"": ""Specific areas to improve""
                    }}";
            }
            else
            {
                prompt = $@"
                    You are a professional teacher grading a student's answer.
                    
                    No rubric is provided.
                    Grade fairly using general academic standards.
                    
                    Student Answer:
                    {request.StudentAnswers}
                    
                    Return ONLY valid JSON in this exact structure with no extra text:
                    
                    {{
                      ""grammar"": number (0-100),
                      ""clarity"": number (0-100),
                      ""content"": number (0-100),
                      ""totalMarks"": number (0-100),
                      ""feedback"": ""Constructive feedback"",
                      ""strength"": ""Strongest point"",
                      ""weakness"": ""Weakest point"",
                      ""improvement"": ""Specific areas to improve""
                    }}";
            }

            var requestBody = new
            {
                model = "llama-3.3-70b-versatile",
                messages = new[]
                {
                    new { role = "user", content = prompt }
                },
                temperature = 0.3
            };

            var client = _Client.CreateClient();
            client.Timeout = TimeSpan.FromSeconds(60);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _GroqApiKey);

            var jsonContent = new StringContent(JsonSerializer.Serialize(requestBody),
                                                Encoding.UTF8,
                                                "application/json");

            var response = await client.PostAsync(_GroqUrl, jsonContent);

            if (!response.IsSuccessStatusCode)
            {
                var errorText = await response.Content.ReadAsStringAsync();
                return StatusCode((int)response.StatusCode, new
                {
                    message = "Groq API error",
                    error = errorText
                });
            }

            var responseText = await response.Content.ReadAsStringAsync();

            var groqResponse = JsonSerializer.Deserialize<GroqGenerateResponseDto>(responseText);

            if (groqResponse == null ||
                groqResponse.choices == null ||
                !groqResponse.choices.Any())
            {
                return Ok(new { rawResponse = responseText });
            }

            var content = groqResponse.choices[0].message?.content;

            if (string.IsNullOrWhiteSpace(content))
            {
                return Ok(new { rawResponse = responseText });
            }

            var cleanJson = content
                .Replace("```json", "")
                .Replace("```", "")
                .Trim();

            AIMarkingResponse? markingResponse;

            try
            {
                markingResponse = JsonSerializer.Deserialize<AIMarkingResponse>(
                    cleanJson,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                if (markingResponse == null)
                {
                    return Ok(new { rawResponse = content });
                }
            }
            catch
            {
                return Ok(new { rawResponse = content });
            }

            var dto = new AIMarkingResponseDto
            {
                AssignmentId = assignmentId,
                StudentId = studentId,
                Grammar = markingResponse.Grammar,
                Clarity = markingResponse.Clarity,
                Content = markingResponse.Content,
                TotalMarks = markingResponse.TotalMarks,
                Feedback = markingResponse.Feedback,
                Strength = markingResponse.Strength,
                Weakness = markingResponse.Weakness,
                Improvement = markingResponse.Improvement
            };

            await CheckPlagiarism(assignmentId, studentId);

            await _Assignment.ReturnAIResponseAsync(dto);

            return Ok(markingResponse);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = "Internal server error",
                error = ex.Message
            });
        }
    }

    private async Task<IActionResult> CheckPlagiarism(Guid assignmentId, Guid studentId)
    {
        try
        {
            var request = await _Assignment.GettingAllMarkingRequestByAssignmentId(assignmentId, studentId);

            if (request == null ||
                string.IsNullOrWhiteSpace(request.StudentAnswers))
            {
                return BadRequest("Student answer is required");
            }

            var prompt = $@"
                You are an academic integrity expert and plagiarism detection system.

                Analyze the following student answer for:
                1. Plagiarism - identify any text that appears copied from known sources
                2. AI-generated content - identify text that appears written by AI (ChatGPT, etc.)
                3. Original student work - estimate how much is genuinely the student's own work

                Student Answer:
                {request.StudentAnswers}

                For each matched source, provide the actual URL if you know it, or the most likely source.

                Return ONLY valid JSON in this exact structure with no extra text:

                {{
                  ""plagiarismPercentage"": number (0-100),
                  ""aiGeneratedPercentage"": number (0-100),
                  ""originalWorkPercentage"": number (0-100),
                  ""plagiarismSummary"": ""Detailed explanation of what was plagiarised and from where"",
                  ""aiDetectionSummary"": ""Explanation of AI usage detected and why you think so"",
                  ""overallVerdict"": ""e.g. Mostly Original / Partially Plagiarised / High AI Usage / Fully AI Generated"",
                  ""matchedSources"": [
                    {{
                      ""sourceTitle"": ""Name of source"",
                      ""sourceUrl"": ""https://actual-url-if-known.com"",
                      ""matchPercentage"": number (0-100),
                      ""matchedText"": ""The specific text that matched this source""
                    }}
                  ]
                }}";

            var requestBody = new
            {
                model = "llama-3.3-70b-versatile",
                messages = new[]
                {
                    new
                    {
                        role = "user",
                        content = prompt
                    }
                },
                temperature = 0.1
            };

            var client = _Client.CreateClient();

            client.Timeout = TimeSpan.FromSeconds(60);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _GroqApiKey);

            var jsonContent = new StringContent(JsonSerializer.Serialize(requestBody),Encoding.UTF8,"application/json");

            var response = await client.PostAsync(_GroqUrl, jsonContent);

            if (!response.IsSuccessStatusCode)
            {
                var errorText = await response.Content.ReadAsStringAsync();

                return StatusCode((int)response.StatusCode, new
                {
                    error = errorText,
                    message = "Groq API error"
                });
            }

            var responseText = await response.Content.ReadAsStringAsync();

            var groqResponse = JsonSerializer.Deserialize<GroqGenerateResponseDto>(responseText);

            if (groqResponse?.choices == null ||
                !groqResponse.choices.Any())
            {
                return Ok(new
                {
                    rawResponse = responseText
                });
            }

            var content = groqResponse.choices[0].message?.content;

            if (string.IsNullOrWhiteSpace(content))
            {
                return Ok(new { rawResponse = responseText });
            }

            var cleanJson = content
                .Replace("```json", "")
                .Replace("```", "")
                .Trim();

            PlagiarismAnalysisResponseDto? analysis;

            try
            {
                analysis = JsonSerializer.Deserialize<PlagiarismAnalysisResponseDto>(
                        cleanJson,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        }
                    );

                if (analysis == null)
                {
                    return Ok(new { rawResponse = content });
                }
            }
            catch
            {
                return Ok(new { rawResponse = content });
            }

            var dto = _Mapper.Map<PlagiarismResultDto>(analysis);

            dto.AssignmentId = assignmentId;
            dto.StudentId = studentId;
            dto.AIMarkingResponseId = request.MarkingRequestId;

            dto.MatchedSources = analysis.matchedSources?.Select(s => new MatchedSourceDto
            {
                SourceTitle = s.sourceTitle,
                SourceUrl = s.sourceUrl,
                MatchPercentage = s.matchedPercentage,
                MatchedText = s.matchedText
            })
            .ToList();

            await _Assignment.SavePlagiarismResultsAsync(dto);

            return Ok(dto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = "Internal server error",
                error = ex.Message,
                ex
            });
        }
    }

    [HttpGet("getPlagiarismResult")]
    public async Task<IActionResult> GetPlagiarismResult(Guid assignmentId, Guid studentId)
    {
        try
        {
            var result = await _Assignment.GetPlagiarismResultsAsync(assignmentId, studentId);

            if (result == null)
            {
                return NotFound("No plagiarism result found for this assignment and student.");
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Internal server error", error = ex.Message, ex });
        }
    }
}