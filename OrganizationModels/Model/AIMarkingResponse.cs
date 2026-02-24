using System.ComponentModel.DataAnnotations;

namespace OrganizationModels.Model
{
    public class AIMarkingResponse
    {
        [Key]
        public Guid MarkingResponseId { get; set; }

        public Guid TeacherId { get; set; }

        public Guid StudentId { get; set; }

        public Guid AssignmentId { get; set; }

        public int Grammar { get; set; } = 0;

        public int Clarity { get; set; } = 0;

        public int Content { get; set; } = 0;

        public int TotalMarks { get; set; } 

        public string? Feedback { get; set; }

        public string? Strength { get; set; }

        public string? Weakness { get; set; }

        public string? Improvement { get; set; }
    }
}
