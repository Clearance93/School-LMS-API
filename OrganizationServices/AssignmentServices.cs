using AutoMapper;
using Microsoft.Extensions.Logging;
using OrganizationCore.UnitOfWork;
using OrganizationDTO.Dto;
using OrganizationDTO.Dto.AIDto;
using OrganizationIInterface.IReporitory;
using OrganizationIInterface.IService.Assignments;
using OrganizationModels.Model;
using System.Text.Json;

namespace OrganizationServices
{
    public class AssignmentServices : IAssignmentServiceInterface
    {
        private readonly IUnitOfWork _Unit;
        private readonly IMapper _Mapper;
        private readonly ITeacherRepository _Teacher;

        public AssignmentServices(IMapper mapper,
                                  IUnitOfWork unit,
                                  ITeacherRepository teacher)
        {
            _Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _Unit = unit ?? throw new ArgumentNullException(nameof(unit));
            _Teacher = teacher ?? throw new ArgumentNullException(nameof(_Teacher));
        }

        public async Task<bool> AddAssignmentGradesAsync(AssignmentGradesDto dto)
        {
            try
            {
                var assignmentsGrades = _Mapper.Map<AssignmentGrades>(dto);

                var assignmentSub = await _Unit.AssignmentSubmission.GetByIdAsync(dto.AssignmentSubmissionId);
                var teacherOrganization = await _Unit.Teacher.GetByIdAsync(assignmentSub!.TeacherId);

                if (assignmentSub == null &&
                    teacherOrganization == null)
                {
                    throw new OrganizationCore.Exceptions.InvalidOperationException($"One of the value provided does not exist in the database teacher Id: {assignmentSub!.TeacherId} or organization Id: {teacherOrganization!.OrganizationSetupId} ");
                }

                assignmentsGrades.AssignmentGradesId = Guid.NewGuid();
                assignmentsGrades.GradedDate = DateTime.Now;
                assignmentsGrades.TeacherId = assignmentSub!.TeacherId;
                assignmentsGrades.OrganizationId = teacherOrganization!.OrganizationSetupId;

                await _Unit.AssignmentGrades.AddAsync(assignmentsGrades);

                assignmentSub!.IsCompleted = true;
                assignmentSub.IsSubmitted = false;

                _Unit.AssignmentSubmission.Update(assignmentSub);

                await _Unit.SaveChangeAsync();

                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> AssignmentsSubmissionAsync(AssignmentSubmissionDto dto)
        {
            try
            {
                var rubric = "";

                var student = await _Unit.Student.GetByIdAsync(dto.StudentId);

                if (student == null)
                {
                    throw new OrganizationCore.Exceptions.InvalidOperationException($"The student with Id: {dto.StudentId} does not exist");
                }

                var assignment = await _Unit.Assignments.GetByIdAsync(dto.AssignmentId);

                if (assignment == null)
                {
                    throw new OrganizationCore.Exceptions.InvalidOperationException($"The assignment with the Id: {dto.AssignmentId} does not exists");
                }

                var submission = _Mapper.Map<AssignmentSubmission>(dto);

                submission.AssignmentSubmissionId = Guid.NewGuid();
                submission.SubmissionDate = DateTime.Now;
                submission.IsCompleted = false;
                submission.IsSubmitted = true;
                submission.IsPending = false;
                submission.TeacherId = assignment.TeacherId;

                var text = PDFExtractorService.ExtractTextFromPdf(submission.AssignmentPdfSubmission!);

                if (assignment.TeacherRubricFile != null)
                {
                    rubric = PDFExtractorService.ExtractTextFromPdf(assignment.TeacherRubricFile!);
                } 

                var markingRequest = new AIMarkingRequest
                {
                    MarkingRequestId = Guid.NewGuid(),
                    StudentId = dto.StudentId,
                    TeacherId = assignment.TeacherId,
                    StudentAnswers = text,
                    TeacherRubric = rubric,
                    AssignmentId = assignment.AssignmentId,
                };

                await _Unit.AssignmentSubmission.AddAsync(submission);

                await _Unit.MarkingRequest.AddAsync(markingRequest);

                await _Unit.SaveChangeAsync();

                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> CreateAnAssignmentAsync(AssignmentDto dto)
        {
            try
            {
                var teacher = await _Teacher.GetByIdAsync(dto.TeacherId);
                var organization = await _Unit.Organization.GetByIdAsync(dto.OrganizationId);
                var grades = await _Unit.GradeStream.GetByIdAsync(dto.GradeStreamId);

                if (teacher == null &&
                    organization == null &&
                    grades == null)
                {
                    throw new OrganizationCore.Exceptions.InvalidOperationException($"One or more values does not exist");
                }

                var assignment = _Mapper.Map<Assignment>(dto);

                assignment.AssignmentId = Guid.NewGuid();
                assignment.CreatedDate = DateTime.Now;

                await _Unit.Assignments.AddAsync(assignment);

                await _Unit.SaveChangeAsync();

                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<AssignmentDto>> GetAllAssignmentsCreatedByTeacherAsync(Guid teacherId)
        {
            try
            {
                var teacher = await _Unit.Teacher.GetByIdAsync(teacherId);

                if (teacher == null)
                {
                    throw new OrganizationCore.Exceptions.InvalidOperationException($"The Id: {teacherId} provided does not exists");
                }

                var assignments = await _Unit.Assignments.GetAllTeacherAssignmentCreatedAsync(teacherId);

                return _Mapper.Map<IEnumerable<AssignmentDto>>(assignments);    
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<StudentAssignmentSubmittedDto>> GetAllSubmittedAssignmentsAsync(Guid teacherId)
        {
            try
            {
                var assignmentLists = new List<StudentAssignmentSubmittedDto>();

                var teacher = await _Unit.Teacher.GetByIdAsync(teacherId);

                if (teacher == null)
                {
                    throw new OrganizationCore.Exceptions.InvalidOperationException($"The teacher Id: {teacherId} provided was not found");
                }

                var allAssignments = await _Unit.AssignmentSubmission.GetAllSubmittedAssignmentByTeacherIdAsync(teacherId);

                if (allAssignments == null)
                {
                    throw new OrganizationCore.Exceptions.InvalidOperationException($"No assignments found for teacher with Id:{teacherId}");
                }

                foreach (var assignment in allAssignments)
                {
                    var subAssignment = await _Unit.AssignmentSubmission.GetTeacherAssignmentsAsync(assignment.AssignmentId);

                    var dto = new StudentAssignmentSubmittedDto
                    {
                        AssignmentDescription = assignment.Assignment!.AssignmentDescription,
                        AssignmentTitle = assignment.Assignment.AssignmentTitle,
                        AssignmentId = subAssignment!.AssignmentId,
                        StudentId = subAssignment!.StudentId,
                        StreamName = assignment.Assignment.GradeStream!.StreamName,
                        StudentFullNames = $"{subAssignment.Student!.FirstName} {subAssignment.Student.LastName}",
                        StudentEmail = subAssignment.Student.StudentEmail,
                        Subject = assignment.Assignment.AssignmentSubject,
                        AssignmentFile = assignment.Assignment.AssignmentFile,
                        IsGraded = assignment.IsCompleted,
                        IsSubmitted = assignment.IsSubmitted,
                        AssignmentSubmissionId = assignment.AssignmentSubmissionId
                    };

                    assignmentLists.Add(dto);
                }

                return assignmentLists;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<AssignmentSubmissionDto>> GetAllTeacherAssignedSubmittedAssignmentAsync(Guid teacherId)
        {
            var teacherSubAssignment = await _Unit.AssignmentSubmission.GetAllSubmittedAssignmentByTeacherIdAsync(teacherId);

            return _Mapper.Map<IEnumerable<AssignmentSubmissionDto>>(teacherSubAssignment);
        }

        public async Task<PlagiarismResultDto?> GetPlagiarismResultsAsync(Guid assignmentId, Guid studentId)
        {
            var entity = await _Unit.PlagiarimsResults.GetByAssignmentAndStudentAsync(assignmentId, studentId);

            if (entity == null)
            {
                return null;
            }

            var dto = _Mapper.Map<PlagiarismResultDto>(entity);

            dto.MatchedSources = string.IsNullOrWhiteSpace(entity.MatchedSourceJson)
                ? new List<MatchedSourceDto>()
                : JsonSerializer.Deserialize<List<MatchedSourceDto>>(entity.MatchedSourceJson);

            return dto;
        }

        public async Task<AssignmentSubmissionDto?> GetStudentSubmittedAssignmentByStudentIdAsync(Guid assignmentSubmissionId)
        {
            var subAssignment = await _Unit.AssignmentSubmission.GetByIdAsync(assignmentSubmissionId);

            return _Mapper.Map<AssignmentSubmissionDto?>(subAssignment);
        }

        public async Task<AIMarkingRequestDto> GettingAllMarkingRequestByAssignmentId(Guid assignmentId, Guid studentId)
        {
            var markingRequest = await _Unit.MarkingRequest.ReturningAllMarkingRequest(assignmentId, studentId);
            
            return _Mapper.Map<AIMarkingRequestDto>(markingRequest);
        }

        public async Task<AIMarkingResponseDto> ReturnAIResponseAsync(AIMarkingResponseDto dto)
        {
            var assignment = await _Unit.Assignments.GetByIdAsync(dto.AssignmentId);

            var aiResponse = _Mapper.Map<AIMarkingResponse>(dto);

            aiResponse.MarkingResponseId = Guid.NewGuid();
            aiResponse.TeacherId = assignment!.TeacherId;

            await _Unit.MarkingResponse.AddAsync(aiResponse);

            await _Unit.SaveChangeAsync();

            var response = new AIMarkingResponseDto
            {
                Grammar = aiResponse.Grammar,
                Clarity = aiResponse.Clarity,
                Content = aiResponse.Content,
                TotalMarks = aiResponse.TotalMarks,
                Feedback = aiResponse.Feedback,
                Strength = aiResponse.Strength,
                Weakness = aiResponse.Weakness,
                Improvement = aiResponse.Improvement
            };

            return response;
        }

        public async Task<bool> SavePlagiarismResultsAsync(PlagiarismResultDto dto)
        {
            var entity = _Mapper.Map<PlagiarismResults>(dto);

            entity.DetectedAt = DateTime.Now;
            entity.MatchedSourceJson = JsonSerializer.Serialize(dto.MatchedSources);

            await _Unit.PlagiarimsResults.AddAsync(entity);

            await _Unit.SaveChangeAsync();

            return true;
        }
    }
}
