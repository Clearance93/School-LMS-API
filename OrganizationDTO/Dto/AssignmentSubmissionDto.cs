namespace OrganizationDTO.Dto
{
    public class AssignmentSubmissionDto
    {
        public Guid AssignmentSubmissionId { get; set; }

        public Guid AssignmentId { get; set; }

        public Guid StudentId { get; set; }

        public string? AssignmentPdfSubmission { get; set; }

        public DateTime SubmissionDate { get; set; }
    }
}
