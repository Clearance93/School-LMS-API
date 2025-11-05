using AutoMapper;
using Microsoft.Extensions.Logging;
using OrganizationCore.UnitOfWork;
using OrganizationDTO.Dto;
using OrganizationDTO.Dto.UpdateDto;
using OrganizationIInterface.IReporitory;
using OrganizationIInterface.IService;
using OrganizationModels;

namespace OrganizationServices
{
    public class AdminService : IAdminServiceInterface
    {
        private readonly IUnitOfWork _Unit;
        private readonly ILogger<AdminService> _Logger;
        private readonly IMapper _Mapper;
        private readonly IOrganizationRepositoryInterface _Organization;

        public AdminService(IUnitOfWork unit,
                            ILogger<AdminService> logger,
                            IMapper mapper,
                            IOrganizationRepositoryInterface organization)
        {
            _Unit = unit ?? throw new ArgumentNullException(nameof(unit));
            _Logger = logger ?? throw new ArgumentNullException(nameof(_Logger));
            _Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _Organization = organization ?? throw new ArgumentNullException(nameof(organization));
        }

        public async Task<AdminDto> GetAdminByEmail(string email)
        {
            var admin = await _Unit.Admin.GetAdminByEmail(email);

            return _Mapper.Map<AdminDto>(admin);
        }

        public async Task<AdminDto> GetAdminByIdAsync(Guid id)
        {
            var admin = await _Unit.Admin.GetByIdAsync(id);

            return _Mapper.Map<AdminDto>(admin);
        }

        public async Task<UpdateAdminDto> UpdateAdminAsync(Guid adminId, UpdateAdminDto dto)
        {
            if (adminId == Guid.Empty)
            {
                throw new Exception($"Admin with this Id {adminId} cannot be empty");
            }

            var existingAdmin = await _Unit.Admin.GetByIdAsync(adminId);

            if (existingAdmin == null)
            {
                throw new Exception($"No user found with the Id: {adminId}");
            }

            _Mapper.Map(dto, existingAdmin);

            existingAdmin.UpdatedAt = DateTime.Now;

            _Unit.Admin.Update(existingAdmin);

            await _Unit.SaveChangeAsync();

            return _Mapper.Map<UpdateAdminDto>(existingAdmin);
        }
    }
}
