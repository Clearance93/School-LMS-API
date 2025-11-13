using System.ComponentModel.DataAnnotations;

namespace OrganizationModels.Model.Settings
{
    public class Grade
    {
        [Key]
        public Guid GradeId { get; set; }

        public string? GradeName { get; set; }

        public DateTime CreatedAt { get; set;}

        public DateTime UpdatedAt { get; set;}

        public Guid? OrganizationId { get; set; }

        public OrganizationSetup? Organization { get; set; }
    }
}
