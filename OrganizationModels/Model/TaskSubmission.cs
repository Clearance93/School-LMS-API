using OrganizationEnums;
using System.ComponentModel.DataAnnotations;

namespace OrganizationModels.Model
{
    public class TaskSubmission
    {
        [Key]
        public Guid TaskSumbissionId { get; set; }

        public Guid OrganizationId { get; set; }

        public Guid TeacherId {  get; set; }

        public string? SubjectName { get; set; }

        public DateOnly DueDate { get; set; }

        public int Marks { get; set; }

        public string? Description { get; set; }

        public string? PdfAttachment {  get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? DeletedAt { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsSubmitted { get; set; }

        public TasksEnums SubmissionTaskStatus { get; set; }
    }
}
