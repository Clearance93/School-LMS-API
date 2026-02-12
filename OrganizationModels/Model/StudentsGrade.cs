using System.ComponentModel.DataAnnotations;

namespace OrganizationModels.Model
{
    public class StudentsGrade
    {
        [Key]
        public Guid StudentGradeId { get; set; }

        public Guid OrganizationId { get; set; }

        public Guid StudentId { get; set; }

        public Guid TeacherId { get; set; }

        public Guid StreamGradeId { get; set; }

        public string? TeacherFirstNames { get; set; }

        public string? TeacherLastName { get; set; }

        public string? StudentFirstName { get; set; }

        public string? StudentLastName { get; set; }

        public string? StudentProfilePicture { get; set; }

        public string? Subject { get; set; }

        public string? StreamName { get; set; }

        public DateTime SubjectAddedAt { get; set; }
    }
}
