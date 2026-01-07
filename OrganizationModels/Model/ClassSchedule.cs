using OrganizationEnums;
using OrganizationModels.Model.Settings;
using System.ComponentModel.DataAnnotations;

namespace OrganizationModels.Model
{
    public class ClassSchedule
    {
        [Key]
        public Guid ClassScheduleId { get; set; }

        public DateOnly? Date {  get; set; }

        public TimeOnly? StartTime { get; set; }

        public TimeOnly? EndTime { get; set; }

        public Guid? GradeStreamId { get; set; }
        public GradeStream? GradeStream { get; set; }

        public ScheduleStatus? Status { get; set; }

        public Guid? TeachingClassId {  get; set; }
        public TeachingClass? TeachingClass { get; set; }

        public Guid? OrganizationId { get; set; }

        public Guid? TeacherId { get; set; }

        public AttendanceSession? AttendanceSession { get; set; }
    }
}
