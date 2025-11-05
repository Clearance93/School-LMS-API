using AutoMapper;
using Microsoft.Extensions.Logging;
using OrganizationCore.UnitOfWork;
using OrganizationDTO.Dto;
using OrganizationDTO.Dto.CreateDto;
using OrganizationDTO.Dto.UpdateDto;
using OrganizationIInterface.IService;
using OrganizationModels.Model;

namespace OrganizationServices
{
    public class StudentServices : IStudentServiceInterface
    {
        private readonly IUnitOfWork _Unit;
        private readonly ILogger<StudentServices> _Logger;
        private readonly IMapper _Mapper;

        public StudentServices(IUnitOfWork unit,
                               ILogger<StudentServices> logger,
                               IMapper mapper)
        {
            _Unit = unit ?? throw new ArgumentNullException(nameof(unit));
            _Logger = logger ?? throw new ArgumentNullException(nameof(_Logger));
            _Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> AddNewStudentAsync(CreateStudentDto dto)
        {
            var existingStudent = await _Unit.Student.GetStudentByEmailAsync(dto.StudentEmail!);

            if (existingStudent != null)
            {
                throw new InvalidOperationException($"The user with the email: {dto.StudentEmail} already existi");
            }

            var user = _Mapper.Map<Students>(dto);

            user.IsActive = true;
            user.IsDeleted = false;
            user.CreatedAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;

            await _Unit.Student.AddAsync(user);

            await _Unit.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteStudentAsync(string email)
        {
            try
            {
                var checkStudent = await _Unit.Student.GetStudentByEmailAsync(email);

                if (checkStudent == null)
                {
                    throw new InvalidOperationException($"The email: {checkStudent} provided returned null values");
                }

                checkStudent.IsActive = false;
                checkStudent.IsDeleted = true;
                checkStudent.UpdatedAt = DateTime.Now;

                _Unit.Student.Update(checkStudent);

                await _Unit.SaveChangeAsync();

                return true;
            }
            catch (Exception exception)
            {
                _Logger.LogError($"Failed to get the user with the email: {email} provided");

                throw new Exception(exception.Message);
            }
        }

        public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync()
        {
            try
            {
                var students = await _Unit.Student.GetAllAsync();

                return _Mapper.Map<IEnumerable<StudentDto>>(students);
            }
            catch (Exception exception)
            {
                _Logger.LogError($"Failed to get students, {exception.Message}");

                throw new Exception(exception.Message);
            }
        }

        public async Task<StudentDto> GetStudentByEmailAsync(string email)
        {
            try
            {
                var existingUser = await _Unit.Student.GetStudentByEmailAsync(email);

                if (existingUser == null)
                {
                    throw new InvalidOperationException($"No student found under the provided email: {email}.");
                }

                return _Mapper.Map<StudentDto>(existingUser);
            }
            catch (Exception exception)
            {
                _Logger.LogError($"Could not find student {email}");

                throw new Exception(exception.Message);
            }
        }

        public async Task<bool> UpdateStudentAsync(Guid studentId, UpdateStudentDto dto)
        {
            if (studentId == Guid.Empty)
            {
                throw new InvalidOperationException($"The studentId: {studentId} provided is either empty or defaul");
            }

            var existingStudent = await _Unit.Student.GetByIdAsync(studentId);

            if (existingStudent == null)
            {
                throw new InvalidOperationException($"The is not student found with the provided id: {studentId}");
            }

            _Mapper.Map(dto, existingStudent);

            existingStudent.UpdatedAt = DateTime.Now;

            _Unit.Student.Update(existingStudent);

            await _Unit.SaveChangeAsync();

            return true;
        }
    }
}
