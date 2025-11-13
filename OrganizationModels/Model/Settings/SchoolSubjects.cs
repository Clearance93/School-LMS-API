using System.ComponentModel.DataAnnotations;

namespace OrganizationModels.Model.Settings
{
    public class SchoolSubjects
    {
        [Key]
        public Guid SubjectId { get; set; }

        public Guid CourseStreamId {  get; set; }
        public CourseStreams? CourseStream { get; set; }

        public string? SubjectName { get; set; }

        public string? GradeLevel { get; set; }
    }
}
