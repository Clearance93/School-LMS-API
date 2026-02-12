using System.Data;

namespace OrganizationDTO.Dto
{
    public class StudentDashboardDto
    {
        public Guid StudentId { get; set; }

        public Guid OrganizationId { get; set; }

        public Guid StudentAttendanceId { get; set; }

        public Guid AttendanceSessionId { get; set; }

        public bool IsPresent { get; set; }

        public Guid AssignmentId { get; set; }

        public string? AssignmentTitle { get; set; }

        public string? AssignmentDescription { get; set; }

        public DateTime AssignmentDueDate { get; set; }

        public int AssignmentTotalMarks { get; set; }

        public string? AssignmentSubject { get; set; }

        public DateTime AssignmentCreatedDate { get; set; }

        public Guid AssignmentSubmissionId { get; set; }

        public DateOnly SubmissionDate { get; set; }

        public bool AssignmentCompleted { get; set; }

        public bool AssignmentIsPending { get; set; }
    }
}
