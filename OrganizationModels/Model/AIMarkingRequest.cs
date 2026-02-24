using System.ComponentModel.DataAnnotations;

namespace OrganizationModels.Model
{
    public class AIMarkingRequest
    {
        [Key]
        public Guid MarkingRequestId { get; set; }

        public Guid StudentId { get; set; }

        public Guid TeacherId { get; set; }

        public Guid AssignmentId { get; set; }

        public string? StudentAnswers { get; set; }

        public string? TeacherRubric { get; set; }
    }
}
