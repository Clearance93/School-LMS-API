namespace OrganizationDTO.Dto.Settings
{
    public class SchoolSubjectDto
    {
        public Guid SubjectId { get; set; }

        public Guid CourseStreamId { get; set; }

        public string? SubjectName { get; set; }

        public string? GradeLevel { get; set; }
    }
}
