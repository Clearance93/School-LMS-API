namespace OrganizationDTO.Dto
{
    public class TeacherDashboardViewDto
    {
        public Guid TeacherId { get; set; }

        public string? TeacherName { get; set; }

        public string? TeacherProfilePicture { get; set; }

        public string? TeacherEmail { get; set; }

        public Guid OrganizationId { get; set; }

        public Guid TeachingClassid { get; set; }

        public string? ClassName { get; set; }

        public string? Subject { get; set; }

        public int TotalStudents { get; set; }

        public int DailyPresent { get; set; }

        public int DailyAbsent { get; set; }

        public decimal ClassPerformance { get; set; }

        public TimeSpan? NextClassStartTime { get; set; }

        public TimeSpan? NextClassEndTime { get; set; }

        public Guid AssignmentId { get; set; }

        public string? AssignmentTitle { get; set; }

        public DateTime? AssignmentDueDate { get; set; }

        public string? AssignmentSubject { get; set; }

        public int? AssignmentSubmittedCount { get; set; }

        public int? AssignmentTotalStudents { get; set; }

        public string AssignmentProgress =>
            AssignmentSubmittedCount.HasValue && AssignmentTotalStudents.HasValue
            ? $"{AssignmentSubmittedCount}/{AssignmentTotalStudents}"
            : "0/0";
    }
}
