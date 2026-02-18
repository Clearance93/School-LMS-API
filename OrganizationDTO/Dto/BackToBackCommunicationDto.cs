namespace OrganizationDTO.Dto
{
    public class BackToBackCommunicationDto
    {
        public Guid BackToBackCommunicationId { get; set; }

        public string? Emojis { get; set; }

        public string? File { get; set; }

        public string? VoiceNote { get; set; }

        public string? Text { get; set; }

        public Guid SenderId { get; set; }

        public Guid ReceiverId { get; set; }

        public Guid OrganizationId { get; set; }

        public DateOnly DateStamp { get; set; }

        public TimeOnly TimeStamp { get; set; }

        public Guid MessageId { get; set; }

        public bool IsRead { get; set; }

        public bool IsBroadcast { get; set; }
    }
}
