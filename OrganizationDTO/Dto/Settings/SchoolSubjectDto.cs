namespace OrganizationDTO.Dto.Settings
{
    public class SchoolSubjectDto
    {
        public Guid SubjectId { get; set; }

        public string? SubjectName { get; set; }

        public string? GradeLevel { get; set; }

        public Guid GradeId { get; set; }

        public Guid OrganizationId { get; set; }
    }
}
