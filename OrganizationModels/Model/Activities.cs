using System.ComponentModel.DataAnnotations;

namespace OrganizationModels.Model
{
    public class Activities
    {
        [Key]
        public Guid ActivityId { get; set; }

        public Guid OrganizationId { get; set; }

        public Guid UserId { get; set; }

        public string? FullName { get; set; }

        public string? ActionDescription { get; set; }

        public string? ActivityType { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsActive { get; set; }
    }
}
