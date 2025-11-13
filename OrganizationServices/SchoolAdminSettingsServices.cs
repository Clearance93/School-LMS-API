using AutoMapper;
using Microsoft.Extensions.Logging;
using OrganizationCore.UnitOfWork;
using OrganizationDTO.Dto;
using OrganizationIInterface.IService;
using OrganizationModels.Model.Settings;

namespace OrganizationServices
{
    public class SchoolAdminSettingsServices : ISchoolAdminSettingsServiceInterface
    {
        private readonly IUnitOfWork _Unit;
        private readonly ILogger<AdminService> _Logger;
        private readonly IMapper _Mapper;

        public SchoolAdminSettingsServices(IMapper mapper,
                                           ILogger<AdminService> logger,
                                           IUnitOfWork unit)
        {
            _Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _Unit = unit ?? throw new ArgumentNullException(nameof(_Unit));
        }

        public async Task<bool> AddGeneralSettingsAsync(SchoolAdminSettingsDto dto)
        {
            var organization = await _Unit.Organization.GetByIdAsync(dto.OrganizationId);

            if (organization == null)
            {
                throw new Exception("Organization not found"); 
            }

            var existingSettings = await _Unit.SchoolAdminSettings.GetSchoolAdminSettingsByIsAsync(dto.OrganizationId);
            var email = organization.AdminEmail;

            if (existingSettings == null)
            {
                var newSettings = _Mapper.Map<SchoolAdminSettings>(dto);

                newSettings.CreatedAt = DateTime.Now;
                newSettings.UpdatedAt = DateTime.Now;
                newSettings.ContactEmail = email;
               
                await _Unit.SchoolAdminSettings.AddAsync(newSettings);
            }
            else
            {
                _Mapper.Map(dto, existingSettings);
                existingSettings.UpdatedAt = DateTime.Now;
                _Unit.SchoolAdminSettings.Update(existingSettings);
            }

            await _Unit.SaveChangeAsync();

            return true;
        }

        public async Task<SchoolAdminSettingsDto> GetGeneralAdminSchoolSettingsAsyc(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    _Logger.LogWarning($"The Id: {id} cannot be empty or default");

                    throw new ArgumentException("Please provide the proper Id");
                }

                var schoolAdmin = await _Unit.SchoolAdminSettings.GetSchoolAdminSettingsByIsAsync(id);

                if (schoolAdmin == null)
                {
                    _Logger.LogInformation($"Failed to get the guest{nameof(schoolAdmin)}");
                }

                var schoolAdminDto = _Mapper.Map<SchoolAdminSettingsDto>(schoolAdmin);

                return schoolAdminDto;
            }
            catch (Exception exception)
            {
                _Logger.LogError($"Failed to fetch the guest with the id of {id}, exception: {exception.Message}");

                throw new Exception(exception.Message);
            }
        }
    }
}
