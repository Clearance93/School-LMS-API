using System.ComponentModel.DataAnnotations;

namespace OrganizationStatistics
{
    public class MessagingStatistics
    {
        public int TotalMessageent {  get; set; }

        [Range(0, 100)]
        public decimal SmsDeliveryRate { get; set; }

        [Range(0, 100)]
        public decimal EmailDeliveryRate { get; set; }

        public int FailedMessage {  get; set; }
    }
}
