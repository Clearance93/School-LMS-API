using System.ComponentModel.DataAnnotations;

namespace OrganizationModels.Model.Settings
{
    public class SchoolSubjects
    {
        [Key]
        public Guid SubjectId { get; set; }

        public string? SubjectName { get; set; }

        public Guid GradeId { get; set; }
        public Grade? Grade { get; set; }

        public Guid OrganizationId { get; set; }
    }
}
