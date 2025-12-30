namespace OrganizationDTO.Dto.Communication
{
    public class CreateMessageDto
    {
        public Guid CreateMessageId { get; set; }

        public string? SenderEmail { get; set; }

        public Guid OrganizationId { get; set; }
 
        public Guid RecipientId { get; set; }

        public string? RecipientRole { get; set; }

        public string? Subject { get; set; }

        public string? Content { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
