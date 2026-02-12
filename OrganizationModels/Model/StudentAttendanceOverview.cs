using System.ComponentModel.DataAnnotations;

namespace OrganizationModels.Model
{
    public class StudentAttendanceOverview
    {
        [Key]
        public Guid StudentAttendanceId { get; set; }

        public Guid OrganizationId { get; set; }

        public Guid StudentId { get; set; }

        public int PresentCount { get; set; }

        public int AbsentCount { get; set; }

        public int LateCount { get; set; }

        public int TermAttendanceOverview {  get; set; }
    }
}
