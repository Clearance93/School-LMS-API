using OrganizationDTO.Dto;
using OrganizationDTO.Dto.Settings;

namespace OrganizationIInterface.IService
{
    public interface ITeacherScheduleInterfaceService
    {
        Task<bool> CreateClassScheduleAsync(ClassScheduleDto dto);

        Task<bool> CreateStudentAttendance(StudentAttendanceDto dto);

        Task<bool> CreateTeachingClass(TeachingClassDto dto);

        Task<IEnumerable<GradeWithStreamDto>> GetGradeStreamByTeacgerIdAsync(Guid teacherId);

        Task<IEnumerable<ClassScheduleDto>> GetAllClassSchedulesAsync(Guid organizationId, Guid teacherId);

        Task<IEnumerable<StudentAttendanceDto>> GetAllStudentAttendancesAsync(Guid organizationId, Guid teacherId);

        Task<IEnumerable<TeachingClassDto>> GetAllTeachingClassAsync(Guid organizationId, Guid teacherId);

        Task<bool> UpdateStudentAttenceDto(Guid id, StudentAttendanceDto dto);

        Task RebuildDailyAttendanceOverViewAsync(Guid organizationId, Guid teacherId, DateTime date);
    }
}
