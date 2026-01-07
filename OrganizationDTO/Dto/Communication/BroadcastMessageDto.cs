namespace OrganizationDTO.Dto.Communication
{
    public class BroadcastMessageDto
    {
        public Guid BroadcastId { get; set; }

        public string Content { get; set; }

        public string? SenderEmail { get; set; }

        public string? Role { get; set; }

        public string? Subject { get; set; }

        public Guid OrganizationId { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}