using AutoMapper;
using Microsoft.Extensions.Logging;
using OrganizationCore.UnitOfWork;
using OrganizationDTO.Dto;
using OrganizationIInterface.IService;
using OrganizationModels.Model;

namespace OrganizationServices
{
    public class TeacherScheduleService : ITeacherScheduleInterfaceService
    {
        private readonly IUnitOfWork _Unit;
        private readonly IMapper _Mapper;
        private readonly ILogger<TeacherScheduleService> _Logger;

        public TeacherScheduleService(ILogger<TeacherScheduleService> logger,
                                      IMapper mapper,
                                      IUnitOfWork unit)
        {
            _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _Mapper = mapper ?? throw new ArgumentNullException(nameof(_Mapper));
            _Unit = unit ?? throw new ArgumentNullException(nameof(unit));
        }

        public async Task<bool> CreateClassScheduleAsync(ClassScheduleDto dto)
        {
            try
            {
                var organization = await _Unit.Organization.GetByIdAsync(dto.OrganizationId);

                if (organization == null)
                {
                    throw new OrganizationCore.Exceptions.InvalidOperationException($"The organizationId :{dto.OrganizationId} provided does not exist");
                }

                var teacherOrg = await _Unit.Teacher.GetTeacherByOrganization(dto.OrganizationId, dto.TeacherId);

                if (teacherOrg == null)
                {
                    throw new OrganizationCore.Exceptions.InvalidOperationException($"The value given {dto.OrganizationId} or {dto.TeacherId} are invalid");
                }

                var teachingClass = await _Unit.TeachingClass.GetByIdAsync(dto.TeachingClassId);

                if (teachingClass == null)
                {
                    throw new OrganizationCore.Exceptions.InvalidOperationException($"The teaching class was not found");
                }

                var attendanceSession = await _Unit.Teacher.GetTeacherByOrganization(dto.OrganizationId, dto.TeacherId);

                var classSchedule = _Mapper.Map<ClassSchedule>(dto);

                classSchedule.ClassScheduleId = Guid.NewGuid();

                await _Unit.ClassSchedule.AddAsync(classSchedule);

                var attendance = new AttendanceSession
                {
                    AttendanceSessionId = Guid.NewGuid(),
                    Date = dto.Date,
                    ClassScheduleId = classSchedule.ClassScheduleId,
                    OrganizationId = dto.OrganizationId,
                    TeacherId = dto.TeacherId,
                };

                await _Unit.AttendanceSession.AddAsync(attendance);

                await _Unit.SaveChangeAsync();

                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> CreateStudentAttendance(StudentAttendanceDto dto)
        {
            try
            {
                var date = DateTime.Now;

                var organization = await _Unit.Organization.GetByIdAsync(dto.OrganizationId);

                if (organization == null)
                {
                    throw new OrganizationCore.Exceptions.InvalidOperationException($"The organizationId :{dto.OrganizationId} provided does not exist");
                }

                var teacherOrg = await _Unit.Teacher.GetTeacherByOrganization(dto.OrganizationId, dto.TeacherId);

                if (teacherOrg == null)
                {
                    throw new OrganizationCore.Exceptions.InvalidOperationException($"The value given {dto.OrganizationId} or {dto.TeacherId} are invalid");
                }

                var attendanceSession = await _Unit.AttendanceSession.GetByIdAsync(dto.AttendanceSessionId);

                if (attendanceSession == null)
                {
                    throw new OrganizationCore.Exceptions.InvalidOperationException($"The value given {dto.AttendanceSessionId} or {dto.TeacherId} are invalid");
                }

                var studentAttendance = _Mapper.Map<StudentAttendance>(dto);

                studentAttendance.StudentAttendanceId = Guid.NewGuid();

                await _Unit.StudentAttendance.AddAsync(studentAttendance);

                await _Unit.SaveChangeAsync();

                await RebuildDailyAttendanceOverViewAsync(dto.OrganizationId, dto.TeacherId, date);

                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> CreateTeachingClass(TeachingClassDto dto)
        {
            try
            {
                var organization = await _Unit.Organization.GetByIdAsync(dto.OrganizationId);

                if (organization == null)
                {
                    throw new OrganizationCore.Exceptions.InvalidOperationException($"The organizationId :{dto.OrganizationId} provided does not exist");
                }

                var teacherOrg = await _Unit.Teacher.GetTeacherByOrganization(dto.OrganizationId, dto.TeacherId);

                if (teacherOrg == null)
                {
                    throw new OrganizationCore.Exceptions.InvalidOperationException($"The value given {dto.OrganizationId} or {dto.TeacherId} are invalid");
                }

                var gradeStream = await _Unit.GradeStream.GetByIdAsync(dto.GradeStreamId);

                if (gradeStream == null)
                {
                    throw new OrganizationCore.Exceptions.InvalidOperationException($"The grade stream with id: {dto.GradeStreamId} does not exist");
                }

                var teaching = _Mapper.Map<TeachingClass>(dto);

                teaching.TeachingClassId = Guid.NewGuid();

                await _Unit.TeachingClass.AddAsync(teaching);

                await _Unit.SaveChangeAsync();

                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<ClassScheduleDto>> GetAllClassSchedulesAsync(Guid organizationId, Guid teacherId)
        {
            var allClassScheduled = await _Unit.ClassSchedule.GetAllClassScheduledAsync(organizationId, teacherId);

            return _Mapper.Map<IEnumerable<ClassScheduleDto>>(allClassScheduled);
        }

        public async Task<IEnumerable<StudentAttendanceDto>> GetAllStudentAttendancesAsync(Guid organizationId, Guid teacherId)
        {
            var allStudentAttendance = await _Unit.StudentAttendance.GertAllStudentAttendance(organizationId, teacherId);

            return _Mapper.Map<IEnumerable<StudentAttendanceDto>>(allStudentAttendance);
        }

        public async Task<IEnumerable<TeachingClassDto>> GetAllTeachingClassAsync(Guid organizationId, Guid teacherId)
        {
            var allTeachingClasses = await _Unit.TeachingClass.GetAllTeachingClassesAsync(organizationId, teacherId);

            return _Mapper.Map<IEnumerable<TeachingClassDto>>(allTeachingClasses);
        }

        public async Task RebuildDailyAttendanceOverViewAsync(Guid organizationId, Guid teacherId, DateTime date)
        {
            await _Unit.AttendanceOverview.RebuildDailyAttendanceOverViewAsync(organizationId, teacherId, date);
        }

        public Task<bool> UpdateStudentAttenceDto(Guid id, StudentAttendanceDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
