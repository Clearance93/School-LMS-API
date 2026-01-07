using OrganizationModels.Model.Settings;
using System.ComponentModel.DataAnnotations;

namespace OrganizationModels.Model
{
    public class AttendanceOverview
    {
        [Key]
        public Guid AttendanceOverviewId { get; set; }

        public Guid OrganizationId { get; set; }

        public Guid TeacherId { get; set; }

        public Guid GradeStreamId { get; set; }

        public Guid TeachingClassId { get; set; }

        public int DailyPresent { get; set; }

        public int DailyAbsent { get; set; }

        public DateOnly Date { get; set; }
    }
}
