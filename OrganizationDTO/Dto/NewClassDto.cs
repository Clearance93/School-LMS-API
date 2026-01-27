namespace OrganizationDTO.Dto
{
    public class NewClassDto
    {
        public Guid NewClassId { get; set; }

        public Guid OrganizationId { get; set; }

        public Guid TeacherId { get; set; }

        public Guid GradeStreamId { get; set; }

        public string? Subject { get; set; }

        public string? ClassRoom { get; set; }

        public int Capacity { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsActiveClass { get; set; }
    }
}
