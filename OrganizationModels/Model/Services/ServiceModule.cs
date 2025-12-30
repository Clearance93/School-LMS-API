using OrganizationEnums;
using System.ComponentModel.DataAnnotations;

namespace OrganizationModels.Model.Services
{
    public class ServiceModule
    {
        [Key]
        public Guid ServiceModuleId { get; set; }

        public Guid OrganizationId { get; set; }
        public OrganizationSetup? Organization {  get; set; }

        public string? ServiceType { get; set; }

        public string? ServiceName { get; set; }

        public string? ServiceDescription { get; set; }

        public string? ServiceIcon {  get; set; }

        public ServiceCategory? ServiceCategory { get; set; }

        public bool? Enabled { get; set; }

        public ServiceConfiguration? COnfiguration { get; set; }

        public ServiceStatistics? Statistics { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}
