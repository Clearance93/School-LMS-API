using AutoMapper;
using OrganizationCore.UnitOfWork;
using OrganizationDTO.Dto;
using OrganizationDTO.Dto.CreateDto;
using OrganizationDTO.Dto.UpdateDto;
using OrganizationIInterface.IService;
using OrganizationModels.Model;
using System.Threading.Tasks;

namespace OrganizationServices
{
    public class OrganizationService : IOrganizationServiceInterface
    {
        private readonly IUnitOfWork _Unit;
        private readonly IMapper _Mapper;

        public OrganizationService(IUnitOfWork unit,
                                   IMapper mapper)
        {
            _Unit = unit ?? throw new ArgumentNullException(nameof(unit));
            _Mapper = mapper;
        }

        public async Task<bool> AddOrganizationAsync(CreateOrganizationDto dto)
        {
            var userData = await _Unit.Users.GetUserByEmailAsync(dto.AdminEmail!);

            if (userData == null)
            {
                throw new Exception($"user with this email: {dto.AdminEmail} does not exist");
            }

            var organization = _Mapper.Map<OrganizationSetup>(dto);

            organization.OrganizationSetupId = Guid.NewGuid();
            organization.IsActive = true;
            organization.IsDeleted = false;

            var admin = _Mapper.Map<Admins>(userData);

            admin.AdminId = Guid.NewGuid();
            admin.IsDeleted = false;
            admin.IsActive = true;
            admin.IsSuperAdmin = true;
            admin.AdminProfilePicture = userData.ProfileImage;
            admin.AdminBusinessEmail = userData.Email;
            admin.CreatedAt = DateTime.Now;
            admin.UpdatedAt = DateTime.Now;
            admin.OrganizationSetupId = organization.OrganizationSetupId;

            await _Unit.Organization.AddAsync(organization);
            await _Unit.Admin.AddAsync(admin);

            await _Unit.SaveChangeAsync();

            return true;
        }

        public async Task<IEnumerable<OrganizationSetupDto>> GetAllOrganizationServiceAsync()
        {
            var organization = await _Unit.Organization.GetAllAsync();
            
            return _Mapper.Map<IEnumerable<OrganizationSetupDto>>(organization);    
        }

        public async Task<OrganizationSetupDto> GetOrganizationByIdAsync(Guid Id)
        {
            var getOrganization = await _Unit.Organization.GetByIdAsync(Id);

            return _Mapper.Map<OrganizationSetupDto>(getOrganization);
        }

        public async Task<UpdateOrganizationDto> UpdateOrganizationAsync(Guid id, UpdateOrganizationDto dto)
        {
            var existingOrganization = await _Unit.Organization.GetByIdAsync(id);

            if (existingOrganization == null || existingOrganization.IsDeleted != true)
            {
                throw new InvalidOperationException($"No Organization was found with the Id: {id}");
            }

            _Mapper.Map<OrganizationSetupDto>(existingOrganization);

            existingOrganization.UpdatedAt = DateTime.Now;

            _Unit.Organization.Update(existingOrganization);

            await _Unit.SaveChangeAsync();

            return _Mapper.Map<UpdateOrganizationDto>(dto);
        }
    }
}
