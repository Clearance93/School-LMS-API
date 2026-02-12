using System.ComponentModel.DataAnnotations;

namespace OrganizationModels.Model
{
    public class AssignmentGrades
    {
        [Key]
        public Guid AssignmentGradesId { get; set; }

        public Guid AssignmentSubmissionId { get; set; }
        public AssignmentSubmission? AssignmentSubmission { get; set; }
        
        public int Marks { get; set; }

        public DateTime GradedDate { get; set; }

        public Guid StudentId { get; set; }

        public string? Subject { get; set; }

        public Guid OrganizationId { get; set; }

        public Guid TeacherId { get; set; }
    }
}
