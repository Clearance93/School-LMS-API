namespace OrganizationDTO.Dto.Settings
{
    public class CourseStreamDto
    {
        public Guid CourseStreamId { get; set; }

        public Guid OrganizationId { get; set; }

        public string? CourseStreamName { get; set; }

        public string? Description { get; set; }
    }
}
