using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace OrganizationModels.Model.Services
{
    public class ServiceStatistics
    {
        [Key]
        public Guid? StatisticsId { get; set; }

        [Required]
        public Guid? ServiceModuleId { get; set; }

        public int TotalRecords { get; set; }

        public int ActiveUsers { get; set; }

        public DateTime lastActivityDate {  get; set; }

        public string? UsegeMatricsJson { get; set; } = "{}";

        public Dictionary<string, object> UsageMetrics
        {
            get => JsonSerializer.Deserialize<Dictionary<string, object>>(UsegeMatricsJson!)
                ?? new Dictionary<string, object>();
            set => UsegeMatricsJson = JsonSerializer.Serialize(value);  
        }
    }
}
