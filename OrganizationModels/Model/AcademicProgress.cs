using System.ComponentModel.DataAnnotations;

namespace OrganizationModels.Model
{
    public class AcademicProgress
    {
        [Key]
        public Guid AcademicProgressId { get; set; }

        public Guid OrganizationId { get; set; }

        public Guid StudentId { get; set; }

        public string? SchoolTerm { get; set; }

        public bool IsCurrentTerm { get; set; }

        public string? Subject { get; set; }

        public int Percentage { get; set; }

        public int OverallPerfomance { get; set; }
    }
}
