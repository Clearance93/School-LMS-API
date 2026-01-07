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
    }
}
