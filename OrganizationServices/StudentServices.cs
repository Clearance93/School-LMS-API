using AutoMapper;
using Microsoft.Extensions.Logging;
using OrganizationCore.Paasword;
using OrganizationCore.UnitOfWork;
using OrganizationDTO;
using OrganizationDTO.Dto;
using OrganizationDTO.Dto.CreateDto;
using OrganizationDTO.Dto.UpdateDto;
using OrganizationIInterface.IReporitory;
using OrganizationIInterface.IService;
using OrganizationModels.Model;

namespace OrganizationServices
{
    public class StudentServices : IStudentServiceInterface
    {
        private readonly IUnitOfWork _Unit;
        private readonly ILogger<StudentServices> _Logger;
        private readonly IMapper _Mapper;
        private readonly IUserServiceInterface _User;
        private readonly IPasswordGenerationInterface _Password;
        private readonly ITeachingClassInterfaceRepository _Teacher;

        public StudentServices(IUnitOfWork unit,
                               ILogger<StudentServices> logger,
                               IMapper mapper,
                               IUserServiceInterface user,
                               IPasswordGenerationInterface password,
                               ITeachingClassInterfaceRepository teacher)
        {
            _Unit = unit ?? throw new ArgumentNullException(nameof(unit));
            _Logger = logger;
            _Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _User = user ?? throw new ArgumentNullException(nameof(user));
            _Password = password ?? throw new ArgumentNullException(nameof(password));
            _Teacher = teacher ?? throw new ArgumentNullException(nameof(teacher));
        }

        public async Task<bool> AddNewStudentAcademicProgressAsync(AcademicProgressDto dto)
        {
            try
            {
                var student = await _Unit.Student.GetByIdAsync(dto.StudentId);
                if (student == null)
                {
                    throw new OrganizationCore.Exceptions.InvalidOperationException(
                        $"The id: {dto.StudentId} does not match any student");
                }

                var subjectPerformance = await _Unit.AssignmentGrades.GetAllAssignmentByStudentIdAsync(dto.StudentId);
                
                if (subjectPerformance == null || !subjectPerformance.Any())
                {
                    return false;
                }

                var subjectAverages = subjectPerformance.Where(g => g.Marks > 0)
                                                        .GroupBy(g => g.Subject)
                                                        .Select(group => new
                                                        {
                                                            Subject = group.Key,
                                                            AverageMark = (int)Math.Round(group.Average(g => g.Marks), 0)  
                                                        })
                                                        .ToList();

                var overallPerformance = subjectAverages.Any()
                    ? (int)Math.Round(subjectAverages.Average(s => (double)s.AverageMark), 0)  
                    : 0;

                var currentSubjectAverage = subjectAverages.FirstOrDefault(s => s.Subject == dto.Subject);
                if (currentSubjectAverage == null)
                {
                    return false;
                }

                var existingRecord = await _Unit.AcademicProgress.FindAsync(ap => ap.StudentId == dto.StudentId
                                                                                  && ap.Subject == dto.Subject
                                                                                  && ap.SchoolTerm == dto.SchoolTerm);

                var existingAcademic = existingRecord?.FirstOrDefault();

                if (existingAcademic == null)
                {
                    var academic = _Mapper.Map<AcademicProgress>(dto);
                    academic.AcademicProgressId = Guid.NewGuid();
                    academic.Percentage = currentSubjectAverage.AverageMark;
                    academic.OverallPerfomance = overallPerformance;
                    await _Unit.AcademicProgress.AddAsync(academic);
                }
                else
                {
                    existingAcademic.Percentage = currentSubjectAverage.AverageMark;
                    existingAcademic.OverallPerfomance = overallPerformance;
                    existingAcademic.IsCurrentTerm = dto.IsCurrentTerm;
                    _Unit.AcademicProgress.Update(existingAcademic);
                }

                await UpdateAllSubjectsOverallPerformance(dto.StudentId, dto.SchoolTerm, overallPerformance);
                await _Unit.SaveChangeAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        private async Task UpdateAllSubjectsOverallPerformance(Guid studentId, string schoolTerm, int overallPerformance)  
        {
            var allStudentRecords = await _Unit.AcademicProgress
                .FindAsync(ap => ap.StudentId == studentId && ap.SchoolTerm == schoolTerm);

            foreach (var record in allStudentRecords)
            {
                record.OverallPerfomance = overallPerformance;
                _Unit.AcademicProgress.Update(record);
            }
        }

        public async Task<bool> AddNewStudentAsync(CreateStudentDto dto)
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

            var existingStudent = await _Unit.Student.GetStudentByEmailAsync(dto.StudentEmail!);

            if (existingStudent != null)
            {
                throw new InvalidOperationException($"The user with the email: {dto.StudentEmail} already existi");
            }

            if (string.IsNullOrWhiteSpace(dto.Password))
            {
                var passwordGeneration = _Password.GeneratePasswordAsync(12);

                var student = new CreateUserDto
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Email = dto.StudentEmail,
                    Password = passwordGeneration,
                    ProfileImage = dto.StudentProfilePicture,
                    Role = "Student"
                };

                await _User.CreateUserAsync(student);
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

        public async Task<bool> AddNewStudentAttendanceAsync(StudentAttendanceOverViewDto dto)
        {
            try
            {
                var student = await _Unit.Student.GetByIdAsync(dto.StudentId);

                if (student == null)
                {
                    throw new OrganizationCore.Exceptions.InvalidOperationException($"The id: {dto.StudentId} does not amtch any student");
                }

                var newStudent = _Mapper.Map<StudentAttendanceOverview>(dto);

                newStudent.StudentAttendanceId = Guid.NewGuid();

                await _Unit.StudentAttendanceOverview.AddAsync(newStudent);

                await _Unit.SaveChangeAsync();

                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> AddStudentSubject(StudentGradeDto dto)
        {
            try
            {
                var student = await _Unit.Student.GetByIdAsync(dto.StudentId);
                var teacher = await _Unit.Teacher.GetByIdAsync(dto.TeacherId);
                var organization = await _Unit.Organization.GetByIdAsync(dto.OrganizationId);
                var stream = await _Unit.GradeStream.GetByIdAsync(dto.StreamGradeId);

                if (student == null ||
                    teacher == null ||
                    organization == null ||
                    stream == null)
                {
                    throw new OrganizationCore.Exceptions.InvalidOperationException("One of the value given is invalid or does not exist.");
                }

                var subject = _Mapper.Map<StudentsGrade>(dto);

                subject.StudentGradeId = Guid.NewGuid();
                subject.SubjectAddedAt = DateTime.Now;
                subject.TeacherFirstNames = teacher.FirstName;
                subject.TeacherLastName = teacher.LastName;

                await _Unit.StudentSubject.AddAsync(subject);

                var streamTeacher = await _Unit.TeachingClass.GetProperTeachingDataAsync(teacher.TeacherId, stream.StreamId);

                if (streamTeacher == null) 
                {
                    throw new OrganizationCore.Exceptions.InvalidOperationException("one the value provided is invalid or does not exist!");
                }

                var availableStudent = await _Unit.StudentSubject.GetStudentGradeByStudentId(dto.StudentId, dto.StreamGradeId);

                if (availableStudent == null)
                {
                    streamTeacher.TotalStudents += 1;

                    _Unit.TeachingClass.Update(streamTeacher);
                }
                else
                {
                    throw new OrganizationCore.Exceptions.InvalidOperationException($"The student {student.FirstName} {student.LastName} already exist in this class");
                }

                await _Unit.SaveChangeAsync();

                return true;
            }
            catch
            {
                throw;
            }
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

        public async Task<bool> DeleteStudentSubjectAsync(Guid studentGradeId)
        {
            var studentSub = await _Unit.StudentSubject.GetByIdAsync(studentGradeId);

            if (studentSub == null)
            {
                throw new OrganizationCore.Exceptions.InvalidOperationException($"The Id: {studentGradeId} provided is invalid or does not exist");
            }

            _Unit.StudentSubject.Delete(studentSub);

            var teacherClass = await _Unit.TeachingClass.GetProperTeachingDataAsync(studentSub.TeacherId, studentSub.StreamGradeId);

            if (teacherClass == null)
            {
                throw new OrganizationCore.Exceptions.InvalidOperationException("The value provided are either invalid or do not exist");
            }

            teacherClass.TotalStudents -= 1;
            
            _Unit.TeachingClass.Update(teacherClass);

            await _Unit.SaveChangeAsync();

            return true;
        }

        public async Task<IEnumerable<AcademicProgressDto>> GetAllAcademicProgressAsync(Guid studentId)
        {
            var acaemics = await _Unit.AcademicProgress.GetStudentAcademicPerfomanceByStudentIdAsync(studentId);

            return _Mapper.Map<IEnumerable<AcademicProgressDto>>(acaemics);
        }

        public async Task<IEnumerable<StudentGradeDto>> GetAllOrganizationStudentGrades(Guid organizationId)
        {
            var orgStudents = await _Unit.StudentSubject.GetAllOrganizationStudents(organizationId);

            return _Mapper.Map<IEnumerable<StudentGradeDto>>(orgStudents);
        }

        public async Task<IEnumerable<StudentAttendanceOverViewDto>> GetAllSTudentAttendanceAsync(Guid studentId)
        {
            var attendance = await _Unit.StudentAttendanceOverview.GetStudentAttendanceByStudentIdAsync(studentId);

            return _Mapper.Map<IEnumerable<StudentAttendanceOverViewDto>>(attendance);
        }

        public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync(Guid organizationId)
        {
            try
            {
                var students = await _Unit.Student.GetAllOrganizationStudentsAsync(organizationId);

                return _Mapper.Map<IEnumerable<StudentDto>>(students);
            }
            catch (Exception exception)
            {
                _Logger.LogError($"Failed to get students, {exception.Message}");

                throw new Exception(exception.Message);
            }
        }

        public async Task<IEnumerable<StudentGradeDto>> GetAllTeacherStudents(Guid teacherId)
        {
            var teacherStudent = await _Unit.StudentSubject.GetAllTeacherStudentsAsync(teacherId);

            return _Mapper.Map<IEnumerable<StudentGradeDto>>(teacherStudent);
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

        public async Task<IEnumerable<StudentScheduledTimetableDto>> StudentScheduledTimetableAsync(Guid studentId)
        {
            var student = await _Unit.Student.GetByIdAsync(studentId);

            if (student == null)
            {
                throw new OrganizationCore.Exceptions.InvalidOperationException($"The Id: {studentId} does not exist or not found");
            }

            return await _Unit.StudentScheduledTimeTable.GetStudentTimeTableAsync(studentId);
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

        public async Task<IEnumerable<StudentGradeDto>> GetAllStudentSubjectByStudentIdAsync(Guid studentId)
        {
            try
            {
                var allSubject = await _Unit.StudentSubject.GetAllStudentsGradeSubjectByStudentIdAsync(studentId);

                return _Mapper.Map<IEnumerable<StudentGradeDto>>(allSubject);
            }
            catch
            {
                throw;
            }
        }
    }
}
