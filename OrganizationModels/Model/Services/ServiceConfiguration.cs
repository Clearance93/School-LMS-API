using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace OrganizationModels.Model.Services
{
    public class ServiceConfiguration
    {
        [Key]
        public Guid ConfigurationId { get; set; }

        public Guid? ServiceModeuleId { get; set; }

        public string? SettingsJson { get; set; } = "{}";

        public Dictionary<string, object> Settings
        {
            get => JsonSerializer.Deserialize<Dictionary<string, object>>(SettingsJson!)
                ?? new Dictionary<string, object>();
            set => SettingsJson = JsonSerializer.Serialize(value);
        }

        public string? LastModifiedBy { get; set; }

        public DateTime? LastModifiedAt { get; set; }
    }
}
