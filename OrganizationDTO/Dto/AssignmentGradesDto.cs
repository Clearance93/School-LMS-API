namespace OrganizationDTO.Dto
{
    public class AssignmentGradesDto
    {
        public Guid AssignmentGradesId { get; set; }

        public Guid AssignmentSubmissionId { get; set; }

        public int Marks { get; set; }

        public DateTime GradedDate { get; set; }

        public Guid StudentId {  get; set; }

        public string? Subject { get; set; }
    }
}
