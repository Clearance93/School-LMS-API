using System.ComponentModel.DataAnnotations;

namespace OrganizationStatistics
{
    public class FeeStatistics
    {
        public decimal TotalCollected { get; set; }

        public decimal TotalOutstanding { get; set; }

        [Range(0, 100)]
        public decimal PercentagePaid { get; set; }

        public int OverdueAccounts { get; set; }
    }
}
