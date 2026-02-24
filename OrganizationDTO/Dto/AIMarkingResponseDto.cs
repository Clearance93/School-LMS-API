namespace OrganizationDTO.Dto
{
    public class AIMarkingResponseDto
    {
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
