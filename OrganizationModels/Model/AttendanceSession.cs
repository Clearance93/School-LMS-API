using System.ComponentModel.DataAnnotations;

namespace OrganizationModels.Model
{
    public class AttendanceSession
    {
        [Key]
        public Guid AttendanceSessionId { get; set; }

        public DateOnly Date {  get; set; }

        public Guid ClassScheduleId { get; set; }

        public Guid OrganizationId { get; set; }

        public Guid TeacherId { get; set; }
    }
}
