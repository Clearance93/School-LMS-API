namespace OrganizationDTO.Dto
{
    public class StudentSessionClassDto
    {
        public string? StreamName { get; set; }

        public string? SubjectName { get; set; }

        public string? ClassUrl { get; set; }

        public string? RoomId { get; set; }

        public string? TeacherFullName { get; set; }

        public int MaxParticipant {  get; set; }

        public string? WorkshopName { get; set; }

        public string? WorkShopDescription { get; set; }

        public DateOnly ScheduledDate { get; set; }

        public TimeOnly ScheduleTime { get; set; }
    }
}