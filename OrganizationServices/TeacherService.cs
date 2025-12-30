using AutoMapper;
using OrganizationCore.Paasword;
using OrganizationCore.UnitOfWork;
using OrganizationDTO;
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
        private readonly IUserServiceInterface _AddnewUser;
        private readonly IPasswordGenerationInterface _Password;


        public TeacherService(IUnitOfWork unit,
                              IMapper mapper,
                              IUserServiceInterface addnewUser,
                              IPasswordGenerationInterface password)
        {
            _Unit = unit ?? throw new ArgumentNullException(nameof(unit));
            _Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _AddnewUser = addnewUser ?? throw new ArgumentNullException(nameof(addnewUser));
            _Password = password ?? throw new ArgumentNullException(nameof(password));
        }

        public async Task<bool> AddNewTeacherAsync(CreateTeacherDto dto)
        {
            var link = await _Unit.RegistrationLink.GetByIdAsync(dto.RegistrationLinkId);

            if (link != null)
            {
                link.UserCount++;

                if (link.UserCount <= link.MaxUsers)
                {
                    _Unit.RegistrationLink.Update(link);
                }
                else
                {
                    throw new OrganizationCore.Exceptions.InvalidOperationException($"The link is broken due to max limit of {link.MaxUsers}");
                }
            }

            var existingUser = await _Unit.Teacher.GetTeacherByEmailAsync(dto.TeacherEmail!);

            if (existingUser != null)
            {
                throw new OrganizationCore.Exceptions.InvalidOperationException($"The email {dto.TeacherEmail} already exist and it belongs to: \n {dto.FirstName} {dto.LastName}");
            }

            if (dto.Password == null)
            {
                var generativePassword = _Password.GeneratePasswordAsync(12);

                var teacher = new CreateUserDto
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Email = dto.TeacherEmail,
                    ProfileImage = dto.TeacherProfilePicture,
                    Password = generativePassword,
                    Role = "Teacher"
                };

                await _AddnewUser.CreateUserAsync(teacher);
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

        public async Task<IEnumerable<TeachersDto>> GetAllTeachersAsync(Guid id)
        {
            var teachers = await _Unit.Teacher.GetAllOrganizationTeachersAsync(id);

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
