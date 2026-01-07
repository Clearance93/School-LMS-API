namespace OrganizationDTO.Dto
{
    public class ActivitiesDto
    {
        public Guid ActivityId { get; set; }

        public Guid OrganizationId { get; set; }

        public string? Email { get; set; }

        public string? FullName { get; set; }

        public string? ActionDescription { get; set; }

        public string? ActivityType { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsActive { get; set; }
    }
}
