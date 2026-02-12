namespace OrganizationDTO.Dto
{
    public class StudentScheduledTimetableDto
    {
        public string? TeacherFirstNames { get; set; }

        public string? TeacherLastName { get; set; }

        public Guid StudentId { get; set; }

        public string? Subject { get; set; }

        public string? StreamName { get; set; }

        public string? SubjectAddedAt { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public string? ClassRoomNumber { get; set; }
    }
}
