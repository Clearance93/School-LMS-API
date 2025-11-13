using System.ComponentModel.DataAnnotations;

namespace OrganizationModels.Model.Settings
{
    public class GradeStream
    {
        [Key]
        public Guid StreamId { get; set; }

        public Guid TeacherId { get; set; }
        public Teachers? Teacher { get; set; }

        public Guid GradeId { get; set; }
        public Grade? Grade { get; set; }

        public string? StreamName { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public Guid? OrganizationId { get; set; }

        public OrganizationSetup? Organization { get; set; }
    }
}
