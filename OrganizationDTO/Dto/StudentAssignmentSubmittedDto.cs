namespace OrganizationDTO.Dto
{
    public class StudentAssignmentSubmittedDto
    {
        public Guid StudentId { get; set; }

        public Guid AssignmentId {  get; set; }

        public Guid AssignmentSubmissionId { get; set; }

        public string? StudentFullNames { get; set; }

        public string? StudentEmail { get; set; }

        public string? AssignmentTitle { get; set; }

        public string? AssignmentFile { get; set; }

        public string? AssignmentDescription { get; set; }

        public string? StreamName { get; set; }

        public string? Subject { get; set; }

        public bool IsSubmitted { get; set; }

        public bool IsGraded { get; set; }
    }
}
