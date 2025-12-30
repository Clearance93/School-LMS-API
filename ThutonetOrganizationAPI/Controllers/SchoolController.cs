using Microsoft.AspNetCore.Mvc;
using OrganizationDTO.Dto.CreateDto;
using OrganizationDTO.Dto.UpdateDto;
using OrganizationIInterface.IService;
using OrganizationIInterface.IService.School;

namespace ThutonetOrganizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly ITeacherServiceInterface _Teacher;
        private readonly IStudentServiceInterface _Student;
        private readonly ILearnerServiceInterface _Learner;
        private readonly IStuffMemberServiceInterface _StuffMember;
        private readonly IGuestServiceInterface _Guest;
        private readonly ILogger<SchoolController> _Logger;

        public SchoolController(ITeacherServiceInterface teacher, 
                                IStudentServiceInterface student, 
                                ILearnerServiceInterface learner, 
                                IStuffMemberServiceInterface stuffMember, 
                                IGuestServiceInterface guest, 
                                ILogger<SchoolController> logger)
        {
            _Teacher = teacher ?? throw new ArgumentNullException(nameof(teacher));
            _Student = student ?? throw new ArgumentNullException(nameof(student));
            _Learner = learner ?? throw new ArgumentNullException(nameof(learner));
            _StuffMember = stuffMember ?? throw new ArgumentNullException(nameof(stuffMember));
            _Guest = guest ?? throw new ArgumentNullException(nameof(guest));
            _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost("teacher")]
        public async Task<IActionResult> AddNewTeacher([FromBody] CreateTeacherDto dto)
        {
            try
            {
                return Ok(await _Teacher.AddNewTeacherAsync(dto));
            }
            catch (Exception exception)
            {
                _Logger.LogError($"Failed to add tutor. {exception.Message}");

                throw new Exception(exception.Message);
            }
        }

        [HttpPost("student")]
        public async Task<IActionResult> AddNewStudent([FromBody] CreateStudentDto dto)
        {
            try
            {
                var student = await _Student.AddNewStudentAsync(dto);

                return Ok(student);
            }
            catch (Exception exception)
            {
                _Logger.LogError($"Failed to add a new student. {exception.Message}");

                throw new Exception(exception.Message);
            }
        }

        [HttpPost("learner")]
        public async Task<IActionResult> AddNewLearner([FromBody] CreateLearnerDto dto)
        {
            try
            {
                return Ok(await _Learner.AddNewLearnerAsync(dto));
            }
            catch (Exception exception)
            {
                _Logger.LogError($"Faild to add a new learner. {exception.Message}");

                throw new Exception(exception.Message);
            }
        }

        [HttpPost("guest")] 
        public async Task<IActionResult> AddNewGuest([FromBody] CreateGuestDto dto)
        {
            try
            {
                return Ok(await _Guest.AddNewGuestAsync(dto));
            }
            catch (Exception exception)
            {
                _Logger.LogError($"Failed to add a new guest{exception.Message}");

                throw new Exception(exception.Message);
            }
        }

        [HttpPost("stuffMember")]
        public async Task<IActionResult> AddNewStuffMember([FromBody] CreateStuffMemberDto dto)
        {
            try
            {
                return Ok(await _StuffMember.AddNewStuffMemberAsync(dto));
            }
            catch (Exception exception)
            {
                _Logger.LogError($"Failed to add a new stuff. {exception.Message}");

                throw new Exception(exception.Message);
            }
        }

        [HttpPut("update-teacher/{id}")]
        public async Task<IActionResult> UpdatingTeacher(Guid id, [FromBody] UpdateTeacherDto dto)
        {
            try
            {
                return Ok(await _Teacher.UpdateTeacherAsync(id, dto));
            }
            catch (Exception exception)
            {
                _Logger.LogError($"Failed to update {dto.FirstName} {dto.LastName}. {exception.Message}");

                throw new Exception(exception.Message);
            }
        }

        [HttpPut("update-learner/{id}")]
        public async Task<IActionResult> UpdatingLearner(Guid id, [FromBody] UpdateLearnerDto dto)
        {
            try
            {
                return Ok(await _Learner.UpdateLearnerAsync(id, dto));
            }
            catch (Exception exception)
            {
                _Logger.LogError($"Failed to update {dto.FirstName} {dto.LastName}. {exception.Message}");

                throw new Exception(exception.Message);
            }
        }

        [HttpPut("update-student/{id}")]
        public async Task<IActionResult> UpdatingStudent(Guid id, [FromBody] UpdateStudentDto dto)
        {
            try
            {
                return Ok(await _Student.UpdateStudentAsync(id, dto));
            }
            catch (Exception exception)
            {
                _Logger.LogError($"Failed to update {dto.FirstName} {dto.LastName}. {exception.Message}");

                throw new Exception(exception.Message);
            }
        }

        [HttpPut("update-stuff-member/{id}")]
        public async Task<IActionResult> UpdatingStuffMember(Guid id, [FromBody] UpdateStuffMemberDto dto)
        {
            try
            {
                return Ok(await _StuffMember.UpdateStuffMemberAsync(id, dto));
            }
            catch (Exception exception)
            {
                _Logger.LogError($"Failed to update {dto.FirstName} {dto.LastName}. {exception.Message}");

                throw new Exception(exception.Message);
            }
        }

        [HttpPut("update-guest/{id}")]
        public async Task<IActionResult> UpdatingGuest(Guid id, [FromBody] UpdateGuestDto dto)
        {
            try
            {
                return Ok(await _Guest.UpdateGuestAsync(id, dto));
            }
            catch (Exception exception)
            {
                _Logger.LogError($"Failed to update {dto.FirstName} {dto.LastName}. {exception.Message}");

                throw new Exception(exception.Message);
            }
        }

        [HttpGet("getAllTeachers/{organizationId}")]
        public async Task<IActionResult> AllTeachers(Guid organizationId)
        {
            try
            {
                return Ok(await _Teacher.GetAllTeachersAsync(organizationId));
            }
            catch (Exception exception)
            {
                _Logger.LogError($"Failed to retrieve teacher. {exception.Message}");

                throw new Exception(exception.Message);
            }
        }

        [HttpGet("getAllLearners/{organizationId}")]
        public async Task<IActionResult> AllLearners(Guid organizationId)
        {
            try
            {
                return Ok(await _Learner.GetAllLearnersAsync(organizationId));
            }
            catch (Exception exception)
            {
                _Logger.LogError($"Failed to retrieve teacher. {exception.Message}");

                throw new Exception(exception.Message);
            }
        }

        [HttpGet("getAllStudents/{organizationId}")]
        public async Task<IActionResult> AllStudents(Guid organizationId)
        {
            try
            {
                return Ok(await _Student.GetAllStudentsAsync(organizationId));
            }
            catch (Exception exception)
            {
                _Logger.LogError($"Failed to retrieve teacher. {exception.Message}");

                throw new Exception(exception.Message);
            }
        }

        [HttpGet("getAllGuests/{organizationId}")]
        public async Task<IActionResult> AllGuests(Guid organizationId)
        {
            try
            {
                return Ok(await _Guest.GetAllGuestAsync(organizationId));
            }
            catch (Exception exception)
            {
                _Logger.LogError($"Failed to retrieve teacher. {exception.Message}");

                throw new Exception(exception.Message);
            }
        }

        [HttpGet("getAllStuffMembers/{organizationId}")]
        public async Task<IActionResult> AllStuffMembers(Guid organizationId)
        {
            try
            {
                return Ok(await _StuffMember.GetAllStuffMembersAsync(organizationId));
            }
            catch (Exception exception)
            {
                _Logger.LogError($"Failed to retrieve teacher. {exception.Message}");

                throw new Exception(exception.Message);
            }
        }

        [HttpGet("getTeacherByEmail/{email}")]
        public async Task<IActionResult> GetTeacherByEmail(string email)
        {
            try
            {
                return Ok(await _Teacher.GetTeacherByEmailAsync(email));
            }
            catch (Exception exception)
            {
                _Logger.LogError($"Failed to retrieve the user by email. {exception.Message}");

                throw new Exception(exception.Message);
            }
        }

        [HttpGet("getlearnerByEmail/{email}")]
        public async Task<IActionResult> GetLearnerByEmail(string email)
        {
            try
            {
                return Ok(await _Learner.GetLearnerByEmailAsync(email));
            }
            catch (Exception exception)
            {
                _Logger.LogError($"Failed to retrieve the user by email. {exception.Message}");

                throw new Exception(exception.Message);
            }
        }

        [HttpGet("getStudentByEmail/{email}")]
        public async Task<IActionResult> GetStudentByEmail(string email)
        {
            try
            {
                return Ok(await _Student.GetStudentByEmailAsync(email));
            }
            catch (Exception exception)
            {
                _Logger.LogError($"Failed to retrieve the user by email. {exception.Message}");

                throw new Exception(exception.Message);
            }
        }

        [HttpGet("getStuffMemberByEmail/{email}")]
        public async Task<IActionResult> GetStuffMemberByEmail(string email)
        {
            try
            {
                return Ok(await _StuffMember.GetStuffMemberAsync(email));
            }
            catch (Exception exception)
            {
                _Logger.LogError($"Failed to retrieve the user by email. {exception.Message}");

                throw new Exception(exception.Message);
            }
        }

        [HttpDelete("removeTeacher/{email}")]
        public async Task<IActionResult> RemoveTeacher(string email)
        {
            try
            {
                return Ok(await _Teacher.DeleteTeacherAsync(email));
            }
            catch (Exception exception)
            {
                _Logger.LogError($"Failed to remove the user: {exception.Message}");

                throw new Exception(exception.Message);
            }
        }

        [HttpDelete("removeLearner/{email}")]
        public async Task<IActionResult> RemoveLearner(string email)
        {
            try
            {
                return Ok(await _Learner.DeleteLearnerAsync(email));
            }
            catch (Exception exception)
            {
                _Logger.LogError($"Failed to remove the user: {exception.Message}");

                throw new Exception(exception.Message);
            }
        }

        [HttpDelete("removeStudent/{email}")]
        public async Task<IActionResult> RemoveStudent(string email)
        {
            try
            {
                return Ok(await _Student.DeleteStudentAsync(email));
            }
            catch (Exception exception)
            {
                _Logger.LogError($"Failed to remove the user: {exception.Message}");

                throw new Exception(exception.Message);
            }
        }

        [HttpDelete("removeGuest/{email}")]
        public async Task<IActionResult> RemoveGuest(string email)
        {
            try
            {
                return Ok(await _Guest.DeleteGuestAsync(email));
            }
            catch (Exception exception)
            {
                _Logger.LogError($"Failed to remove the user: {exception.Message}");

                throw new Exception(exception.Message);
            }
        }
        
        [HttpDelete("removeStuffMember/{email}")]
        public async Task<IActionResult> RemoveStuffMember(string email)
        {
            try
            {
                return Ok(await _StuffMember.DeleteStuffMemberAsync(email));
            }
            catch (Exception exception)
            {
                _Logger.LogError($"Failed to remove the user: {exception.Message}");

                throw new Exception(exception.Message);
            }
        }
    }
}
