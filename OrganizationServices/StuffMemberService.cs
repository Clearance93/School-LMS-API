using AutoMapper;
using OrganizationCore.UnitOfWork;
using OrganizationDTO.Dto;
using OrganizationDTO.Dto.CreateDto;
using OrganizationDTO.Dto.UpdateDto;
using OrganizationIInterface.IService;
using OrganizationModels.Model;

namespace OrganizationServices
{
    public class StuffMemberService : IStuffMemberServiceInterface
    {
        private readonly IUnitOfWork _Unit;
        private readonly IMapper _Mapper;

        public StuffMemberService(IUnitOfWork unit,
                                 IMapper mapper)
        {
            _Unit = unit ?? throw new ArgumentNullException(nameof(unit));
            _Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> AddNewStuffMemberAsync(CreateStuffMemberDto dto)
        {
            var existingStuffMember = await _Unit.StuffMember.GetStuffMemberByEmailAsync(dto.StuffmemberEmail!);

            if (existingStuffMember != null)
            {
                throw new OrganizationCore.Exceptions.InvalidOperationException($"The email {dto.StuffmemberEmail} already exist and it belongs to: \n {dto.FirstName} {dto.LastName}");
            }

            var user = _Mapper.Map<StuffMembers>(dto);

            user.IsActive = true;
            user.IsDeleted = false;
            user.CreatedAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;

            await _Unit.StuffMember.AddAsync(user);

            await _Unit.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteStuffMemberAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new OrganizationCore.Exceptions.InvalidOperationException($"The email: {email} is either contain white spaces or it is null");
            }

            var existingStuffMember = await _Unit.StuffMember.GetStuffMemberByEmailAsync(email);

            if (existingStuffMember == null) 
            {
                throw new OrganizationCore.Exceptions.InvalidOperationException($"The user: {email} does not exist or is beng removed");
            }

            existingStuffMember.UpdatedAt = DateTime.Now;
            existingStuffMember.IsActive = false;
            existingStuffMember.IsDeleted= false;

            _Unit.StuffMember.Update(existingStuffMember!);

            await _Unit.SaveChangeAsync();

            return true;
        }

        public async Task<IEnumerable<StuffMemberDto>> GetAllStuffMembersAsync()
        {
            var stuffMembers = await _Unit.StuffMember.GetAllAsync();

            return _Mapper.Map<IEnumerable<StuffMemberDto>>(stuffMembers);
        }

        public async Task<StuffMemberDto> GetStuffMemberAsync(string email)
        {
            var user = await _Unit.StuffMember.GetStuffMemberByEmailAsync(email);

            return _Mapper.Map<StuffMemberDto>(user);
        }

        public async Task<bool> UpdateStuffMemberAsync(Guid id, UpdateStuffMemberDto dto)
        {
            if (id == Guid.Empty)
            {
                throw new OrganizationCore.Exceptions.InvalidOperationException($"The privided id: {id}, is either empty or default");
            }

            var existingUser = await _Unit.StuffMember.GetByIdAsync(id);

            if (existingUser == null)
            {
                throw new OrganizationCore.Exceptions.InvalidOperationException($"There is no user found with the Id provided: {id}");
            }

            _Mapper.Map(dto, existingUser);

            _Unit.StuffMember.Update(existingUser);

            await _Unit.SaveChangeAsync();

            return true;
        }
    }
}
