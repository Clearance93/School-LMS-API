using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OrganizationCore.UnitOfWork;
using OrganizationDTO.Dto;
using OrganizationIInterface.IService;
using OrganizationModels.Model;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

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

        public ScheduledMettingsService(IMapper mapper,
                                        IUnitOfWork unit,
                                        IConfiguration configuration,
                                        IHttpClientFactory httpClient)
        {
            _Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _Unit = unit ?? throw new ArgumentNullException(nameof(unit));
            _Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _ApiKey = configuration["Daily:API_Key"];
            _DailyRoomCreationUrl = configuration["Daily:DailyRoomCreationUrl"];
            _HttpClient = httpClient;
        }

        public async Task<bool> CreateAMettingLink(ScheduledWorkshopDto dto)
        {
            try
            {
                var dailyRoomCreation = await CreateARoomSession(dto.WorkshopName);

                var meetings = _Mapper.Map<ScheduledWorkshop>(dto);

                meetings.CreatedAt = DateTime.Now;
                meetings.IsDeleted = false;
                meetings.ScheduledWorkshopId = Guid.NewGuid();
                meetings.MeetingUrl = dailyRoomCreation.MeetingUrl;
                meetings.RoomId = dailyRoomCreation.RoomId;
                meetings.Privacy = dailyRoomCreation.Privacy;
                meetings.Success = dailyRoomCreation.Success;

                await _Unit.ScheduledWorkshop.AddAsync(meetings);

                await _Unit.SaveChangeAsync();

                return true;
            }
            catch
            {
                throw;
            }
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
    }
}
