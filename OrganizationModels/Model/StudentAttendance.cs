using System.ComponentModel.DataAnnotations;

namespace OrganizationModels.Model
{
    public class StudentAttendance
    {
        [Key]
        public Guid StudentAttendanceId { get; set; }

        public Guid? AttendanceSessionId {  get; set; }
        public AttendanceSession? AttendanceSession { get; set; }

        public Guid? StudentId { get; set; }
        public Students? Student {  get; set; }
        
        public bool? IsPresent { get; set; }

        public Guid? OrganizationId { get; set; }

        public Guid? TeacherId { get; set; }
    }
}
