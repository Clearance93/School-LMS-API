namespace OrganizationModels.Model
{
    public class AssignmentSubmission
    {
        public Guid AssignmentSubmissionId { get; set; }

        public Guid AssignmentId { get; set; }
        public Assignment? Assignment { get; set; }

        public Guid StudentId { get; set; }
        public Students? Student { get; set; }

        public string? AssignmentPdfSubmission {  get; set; }

        public DateTime SubmissionDate { get; set; }

        public bool IsPending { get; set; }

        public bool IsSubmitted { get; set; }

        public bool IsCompleted { get; set; }
    }
}
