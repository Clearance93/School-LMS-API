namespace OrganizationDTO.Dto
{
    public class ScheduledWorkshopDto
    {
        public Guid ScheduledWorkshopId { get; set; }

        public Guid OrganizationId { get; set; }

        public Guid? TeacherId { get; set; }

        public Guid? AdminId { get; set; }

        public string? WorkshopName { get; set; }

        public string? WorkShopDescription { get; set; }

        public DateOnly ScheduledDate { get; set; }

        public TimeOnly ScheduleTime { get; set; }

        public int TimeDuration { get; set; }

        public string? RoomId { get; set; }

        public string Privacy { get; set; } = "Public";

        public bool Success { get; set; }

        public string? Thumbnail { get; set; }

        public int MaxParticipants { get; set; }

        public string? MeetingUrl { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime DeletedAt { get; set; }

        public bool IsDeleted { get; set; }
    }
}
