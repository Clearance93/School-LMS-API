using OrganizationModels.Model.Settings;
using System.ComponentModel.DataAnnotations;

namespace OrganizationModels.Model
{
    public class Assignment
    {
        [Key]
        public Guid AssignmentId { get; set; }

        public Guid OrganizationId { get; set; }

        public Guid TeacherId {  get; set; }

        public string? AssignmentTitle { get; set; }

        public string? AssignmentDescription { get; set; }

        public DateTime DueDate { get; set; }

        public int AssignmentMarks { get; set; }

        public Guid GradeStreamId { get; set; }
        public GradeStream? GradeStream { get; set; }

        public string? AssignmentSubject { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? AssignmentFile { get; set; }
    }
}
