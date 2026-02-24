namespace OrganizationDTO.Dto
{
    public class AIMarkingRequestDto
    {
        public Guid MarkingRequestId { get; set; }

        public Guid StudentId { get; set; }

        public Guid TeacherId { get; set; }

        public Guid AssignmentId { get; set; }

        public string? StudentAnswers { get; set; }

        public string? TeacherRubric { get; set; }
    }
}
