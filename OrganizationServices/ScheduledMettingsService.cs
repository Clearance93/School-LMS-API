using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OrganizationCore.UnitOfWork;
using OrganizationDTO.Dto;
using OrganizationIInterface.IService;
using OrganizationModels;
using OrganizationModels.Model;
using OrganizationModels.Model.Communication;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;

namespace OrganizationServices
{
    public class ScheduledMettingsService : IScheduledWorkshopServiceInterface
    {
        private readonly IUnitOfWork _Unit;
        private readonly IMapper _Mapper;
        private readonly string _ApiKey;
        private readonly string _DailyRoomCreationUrl;
        private readonly IConfiguration _Configuration;
        private readonly IHttpClientFactory _HttpClient;
        private readonly UserManager<ApplicationUser> _UserManager;

        public ScheduledMettingsService(IMapper mapper,
                                        IUnitOfWork unit,
                                        IConfiguration configuration,
                                        IHttpClientFactory httpClient,
                                        UserManager<ApplicationUser> userManager)
        {
            _Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _Unit = unit ?? throw new ArgumentNullException(nameof(unit));
            _Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _ApiKey = configuration["Daily:API_Key"];
            _DailyRoomCreationUrl = configuration["Daily:DailyRoomCreationUrl"];
            _HttpClient = httpClient;
            _UserManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<bool> CreateAMettingLink(ScheduledWorkshopDto dto, string subject = null!)
        {
            try
            {
                var dailyRoomCreation = await CreateARoomSession(dto.WorkshopName);

                var meetings = _Mapper.Map<ScheduledWorkshop>(dto);

                var cleaningSubject = CleanSubjectString(subject);

                meetings.CreatedAt = DateTime.Now;
                meetings.IsDeleted = false;
                meetings.ScheduledWorkshopId = Guid.NewGuid();
                meetings.MeetingUrl = dailyRoomCreation.MeetingUrl;
                meetings.RoomId = dailyRoomCreation.RoomId;
                meetings.Privacy = dailyRoomCreation.Privacy;
                meetings.Success = dailyRoomCreation.Success;
                meetings.GradeStreamId = dto.GradeStreamId;

                await _Unit.ScheduledWorkshop.AddAsync(meetings);

                await _Unit.SaveChangeAsync();

                return true;
            }
            catch
            {
                throw;
            }
        }

        private string CleanSubjectString(string? subject)
        {
            if (string.IsNullOrWhiteSpace(subject))
            {
                return string.Empty;
            }

            var match = Regex.Match(subject, @"^([^-]+?)(?:\s*-\s*Grade|\s+Grade)");
            return match.Success ? match.Groups[1].Value.Trim() : subject.Trim();
        }

        private async Task<ScheduledWorkshopDto> CreateARoomSession(string? workshopName)
        {
            var client = _HttpClient.CreateClient();
            var apiUrl = _DailyRoomCreationUrl;

            var sanitizedName = SanitizeRoomName(workshopName);

            var requestbody = new
            {
                name = $"{sanitizedName}-{Guid.NewGuid().ToString("N").Substring(0, 5)}",
                privacy = "public",
                properties = new
                {
                    enable_chat = true,
                    enable_prejoin_ui = true,
                    enable_screenshare = true,
                    enable_emoji_reactions = true,
                    start_video_off = false,
                    start_audio_off = false,
                }
            };

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _ApiKey);
            var json = JsonConvert.SerializeObject(requestbody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var request = await client.PostAsync(apiUrl, content);

            if (request.IsSuccessStatusCode)
            {
                var responseContent = await request.Content.ReadAsStringAsync();
                var dailyResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);
                var response = new ScheduledWorkshopDto
                {
                    RoomId = dailyResponse.id,
                    MeetingUrl = dailyResponse.url,
                    WorkshopName = workshopName,
                    Privacy = dailyResponse.privacy ?? "public",
                    Success = true,
                };
                return response;
            }
            else
            {
                var errorContent = await request.Content.ReadAsStringAsync();
                throw new Exception($"Failed to create the room meeting/session: {errorContent}");
            }
        }

        private string SanitizeRoomName(string? workshopName)
        {
            if (string.IsNullOrWhiteSpace(workshopName))
            {
                return "workshop";
            }

            var sanitized = workshopName.Trim().Replace(" ", "-");

            var result = new StringBuilder();
            foreach (char c in sanitized)
            {
                if (char.IsLetterOrDigit(c) || c == '-' || c == '_')
                {
                    result.Append(c);
                }
            }

            var finalName = result.ToString();
            return string.IsNullOrEmpty(finalName) ? "workshop" : finalName;
        }

        public async Task<IEnumerable<ScheduledWorkshopDto>> GetScheduledWorkshopAsync(Guid organizationId, Guid userId)
        {
            var mettings = await _Unit.ScheduledWorkshop.GetAllMeetingsByUserAsync(organizationId, userId);

            return _Mapper.Map<IEnumerable<ScheduledWorkshopDto>>(mettings);
        }

        public async Task<IEnumerable<object>> GetAllRolesAsync(string email)
        {
            return await _Unit.ScheduledWorkshop.GetAllOrganizationRolesAsync();
        }

        public async Task<IEnumerable<ScheduledWorkshopDto>> GetAllStudentUpomingClassesAsync(Guid studentId)
        {
            try
            {
                var scheduledClasses = new List<ScheduledWorkshopDto>();

                var StudentSubjects = await _Unit.StudentSubject.GetAllStudentsGradeSubjectByStudentIdAsync(studentId);

                if (StudentSubjects == null)
                {
                    throw new OrganizationCore.Exceptions.InvalidOperationException($"The student Id: {studentId} provied does not exist");
                }

                foreach (var studentSubject in StudentSubjects )
                {
                    var studentGrade = await _Unit.ScheduledWorkshop.GetAllStudentUpcomingClassesAsync(studentSubject.StreamGradeId);

                    if (studentGrade == null)
                    {
                        continue;
                    }

                    var dto = new ScheduledWorkshopDto
                    {
                        ScheduledWorkshopId = studentGrade!.ScheduledWorkshopId,
                        OrganizationId = studentGrade.OrganizationId,
                        TeacherId = studentGrade.TeacherId,
                        WorkshopName = studentGrade.WorkshopName,
                        WorkShopDescription = studentGrade.WorkShopDescription,
                        ScheduledDate = studentGrade.ScheduledDate,
                        ScheduleTime = studentGrade.ScheduleTime,
                        TimeDuration = studentGrade.TimeDuration,
                        MaxParticipants = studentGrade.MaxParticipants,
                        MeetingUrl = studentGrade.MeetingUrl,
                        CreatedAt = studentGrade.CreatedAt,
                        IsDeleted = studentGrade.IsDeleted,
                        RoomId = studentGrade.RoomId,
                    };

                    scheduledClasses.Add(dto);
                }

                return scheduledClasses;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<ScheduledWorkshopDto>> GetAllUpcomingWorkshopByRoleAsync(string role)
        {
            var workshop = await _Unit.ScheduledWorkshop.GetAllMeatingsByRole(role);

            return _Mapper.Map<IEnumerable<ScheduledWorkshopDto>>(workshop);
        }

        public async Task<IEnumerable<BackToBackCommunicationDto>> GettingAllChatHistoryAsync(Guid messageId)
        {
            var chatHistrory = await _Unit.BackToBackCommunication.GetAllChatHistoryByMessageIdAsync(messageId);

            return _Mapper.Map<IEnumerable<BackToBackCommunicationDto>>(chatHistrory);
        }

        public async Task<bool> SavingAllChatHostoryAsync(BackToBackCommunicationDto dto)
        {
            try
            {
                var initialMessage = await _Unit.Communication.GetByIdAsync(dto.MessageId);

                if (initialMessage == null)
                {
                    throw new OrganizationCore.Exceptions.InvalidOperationException($"The message Id: {dto.MessageId} does not exist at all");
                }

                var addToChat = _Mapper.Map<BackToBackCommunication>(dto);

                addToChat.IsRead = false;
                addToChat.IsBroadcast = initialMessage.IsBrodacast;

                await _Unit.BackToBackCommunication.AddAsync(addToChat);

                await _Unit.SaveChangeAsync();

                return true;
            }
            catch
            {
                throw;
            }
        }

        //public Task<(byte[] FileBytes, string ContentType, string FileName)> GetTheMediaFilesAsync(string fileName)
        //{
        //    var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "upload");

        //    if (!Directory.Exists(uploadPath))
        //    {
        //        Directory.CreateDirectory(uploadPath);
        //    }

        //    var filePath = Path.Combine(uploadPath, fileName);

        //    if (!File.Exists(filePath))
        //    {
        //        throw new FileNotFoundException($"File '{fileName}' not found in upload directory");
        //    }

        //    var fileBytes = File.ReadAllBytes(filePath);

        //    var contentType = GetContentType(fileName);

        //    return Task.FromResult((fileBytes, contentType, fileName));
        //}

        //private string GetContentType(string fileName)
        //{
        //    var ext = Path.GetExtension(fileName).ToLowerInvariant();

        //    return ext switch
        //    {
        //        ".pdf" => "application/pdf",
        //        ".jpg" or ".jpeg" => "image/jpeg",
        //        ".png" => "image/png",
        //        ".gif" => "image/gif",
        //        ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
        //        ".doc" => "application/msword",
        //        ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        //        ".xls" => "application/vnd.ms-excel",
        //        ".pptx" => "application/vnd.openxmlformats-officedocument.presentationml.presentation",
        //        ".mp4" => "video/mp4",
        //        ".webm" => "video/webm",
        //        ".avi" => "video/x-msvideo",
        //        ".mov" => "video/quicktime",
        //        ".mp3" => "audio/mpeg",
        //        ".wav" => "audio/wav",
        //        ".ogg" => "audio/ogg",
        //        _ => "application/octet-stream"
        //    };
        //}
    }
}
