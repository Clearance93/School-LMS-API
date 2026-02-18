using System.ComponentModel.DataAnnotations;

namespace OrganizationModels.Model.Communication
{
    public class Message
    {
        [Key]
        public Guid MessageId { get; set; }

        public Guid OrganizationId { get; set; }
        public OrganizationSetup? Organization {  get; set; }
        
        public Guid SenderId { get; set; }

        public string? SenderName { get; set; }

        public string? SenderRole {  get; set; }

        public Guid? RecipientId { get; set; }
        
        public string? RecipientName { get; set; }

        public string? RecipientRole { get; set; }

        public string? Subject {  get; set; }

        public string? Content { get; set; }

        public DateTime TimeStamp { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsModified { get; set; }

        public bool IsRead { get; set; }

        public bool IsBrodacast { get; set; }
    }
}
