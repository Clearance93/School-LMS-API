using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OrganizationCore.UnitOfWork;
using OrganizationDTO.Dto.Settings;
using OrganizationIInterface.IService.School;
using OrganizationModels.Model;

namespace OrganizationServices.School
{
    public class RegistrationService : IRegistrationLinkServiceInterface
    {
        private readonly IUnitOfWork _Unit;
        private readonly ILogger<RegistrationService> _Logger;
        private readonly IMapper _Mapper;
        private readonly IConfiguration _Configuration;
        private readonly string _Student;
        private readonly string _Teacher;
        private readonly string _StaffMember;
        private readonly string _Learner;
        private readonly string _Guest;

        public RegistrationService(IMapper mapper,
                                   ILogger<RegistrationService> logger,
                                   IUnitOfWork unit,
                                   IConfiguration configuration)
        {
            _Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _Logger = logger ?? throw new ArgumentNullException(nameof(_Logger));
            _Unit = unit ?? throw new ArgumentNullException(nameof(unit));
            _Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _Student = configuration["RoleRegistrationUrls:StudentUrl"]!;
            _Teacher = configuration["RoleRegistrationUrls:TeacherUrl"]!;
            _StaffMember = configuration["RoleRegistrationUrls:StaffMember"]!;
            _Learner = configuration["RoleRegistrationUrls:Learner"]!;
            _Guest = configuration["RoleRegistrationUrls:Guest"]!;
        }

        public async Task<int> GeAllCountPerregLinkAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return 0;
            }

            var countUsedLink = await _Unit.RegistrationLink.GetByIdAsync(id);

            var usedLink = countUsedLink!.UserCount;

            if (usedLink > 0)
            {
                return usedLink;
            }

            return 0;
        }

        public async Task<string> GetRoleBaseUrlLinkForRegistrationAsync(GeneretingRegistrationLinkDto dto)
        {
            var baseUrl = dto.Role switch
            {
                "Student" => _Student,
                "Teacher" => _Teacher,
                "Staff Member" => _StaffMember,
                "Learner" => _Learner,
                "Guest" => _Guest,
                _ => throw new InvalidOperationException($"Invalid role: {dto.Role}")
            };
            var urlLink = await _Unit.RegistrationLink.GetByIdAsync(dto.RegistrationLinkId);

            var link = _Mapper.Map<RegistrrationLink>(dto);

            var regId = Guid.NewGuid();

            if (urlLink == null)
            {
                link.RegistrationLinkId = regId;
                link.UserCount++;
                link.UrlLink = $"{baseUrl}?organizationId={link.OrganizationId}&linkId={regId}";
                link.IsActive = true;
                link.CreatedAt = DateTime.Now;
                link.UpdatedAt = DateTime.Now;

                await _Unit.RegistrationLink.AddAsync(link);
            }
            else
            {
                if (!urlLink.IsActive)
                {
                    throw new InvalidOperationException("Registration link has been deactivated");
                }
                else if (urlLink.UserCount >= urlLink.MaxUsers)
                {
                    urlLink.IsActive = false;
                    _Unit.RegistrationLink.Update(urlLink);
                    await _Unit.SaveChangeAsync();

                    throw new InvalidOperationException("Registration link has reached maximum users");
                }

                urlLink.UserCount++;
                urlLink.UpdatedAt = DateTime.UtcNow;

                if (urlLink.UserCount >= urlLink.MaxUsers)
                {
                    urlLink.IsActive = false;
                }

            }

            await _Unit.SaveChangeAsync();

            if (link.RegistrationLinkId != Guid.Empty)
            {
                var activeLink = await _Unit.RegistrationLink.GetByIdAsync(link.RegistrationLinkId);

                if (activeLink!.UserCount == 1)
                {
                    return activeLink.UrlLink!;
                }
            }

            var roleLink = await _Unit.RegistrationLink.GetByIdAsync(regId);

            return roleLink!.UrlLink!;
        }
    }
}
