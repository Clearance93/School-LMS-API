using System.ComponentModel.DataAnnotations;

namespace OrganizationModels.Model
{
    public class LeadershipProgram
    {
        [Key]
        public Guid ProgrameId { get; set; }

        public string? ProgramName { get; set; }

        public string? ProgramType { get; set; }

        public string? Description { get; set; }

        public Guid OrganizationId { get; set; }

        public Guid AdminId {  get; set; }

        public DateOnly StartDate { get; set; }

        public int MaxParticipants { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsActive { get; set; }
    }
}
