using AutoMapper;
using OrganizationCore.UnitOfWork;
using OrganizationDTO.Dto;
using OrganizationDTO.Dto.CreateDto;
using OrganizationDTO.Dto.UpdateDto;
using OrganizationIInterface.IService;
using OrganizationModels.Model;

namespace OrganizationServices
{
    public class TeacherService : ITeacherServiceInterface
    {
        private readonly IUnitOfWork _Unit;
        private readonly IMapper _Mapper;

        public TeacherService(IUnitOfWork unit,
                              IMapper mapper)
        {
            _Unit = unit ?? throw new ArgumentNullException(nameof(unit));
            _Mapper = mapper ?? throw new ArgumentNullException(nameof(_Mapper));
        }

        public async Task<bool> AddNewTeacherAsync(CreateTeacherDto dto)
        {
            var existingUser = await _Unit.Teacher.GetTeacherByEmailAsync(dto.TeacherEmail!);

            if (existingUser != null)
            {
                throw new OrganizationCore.Exceptions.InvalidOperationException($"The email {dto.TeacherEmail} already exist and it belongs to: \n {dto.FirstName} {dto.LastName}");
            }

            var user = _Mapper.Map<Teachers>(dto);

            user.IsActive = true;
            user.IsDeleted = false;
            user.CreatedAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;

            await _Unit.Teacher.AddAsync(user);

            await _Unit.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteTeacherAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new OrganizationCore.Exceptions.InvalidOperationException($"Email: {email} is either null or contain white space");
            }

            var existingUser = await _Unit.Teacher.GetTeacherByEmailAsync(email);

            if (existingUser == null)
            {
                throw new OrganizationCore.Exceptions.InvalidOperationException($"No user found with this email: {email}");
            }

            existingUser.IsDeleted = true;
            existingUser.IsActive = false;
            existingUser.UpdatedAt = DateTime.Now;

            _Unit.Teacher.Update(existingUser);

            await _Unit.SaveChangeAsync();

            return true;
        }

        public async Task<IEnumerable<TeachersDto>> GetAllTeachersAsync()
        {
            var teachers = await _Unit.Teacher.GetAllAsync();

            return _Mapper.Map<IEnumerable<TeachersDto>>(teachers);
        }

        public async Task<TeachersDto> GetTeacherByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new OrganizationCore.Exceptions.InvalidOperationException("Please check your email and remove trailing spaces");
            }

            var user = await _Unit.Teacher.GetTeacherByEmailAsync(email);

            return _Mapper.Map<TeachersDto>(user);
        }

        public async Task<bool> UpdateTeacherAsync(Guid id, UpdateTeacherDto dto)
        {
            var user = await _Unit.Teacher.GetByIdAsync(id);

            _Mapper.Map(dto, user);

            user!.IsActive = true;
            user.UpdatedAt = DateTime.Now;
            user.IsDeleted = false;

            _Unit.Teacher.Update(user);

            await _Unit.SaveChangeAsync();

            return true;
        }
    }
}
