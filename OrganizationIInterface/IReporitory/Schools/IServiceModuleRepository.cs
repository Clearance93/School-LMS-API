using OrganizationModels.Model.Services;

namespace OrganizationIInterface.IReporitory.Schools
{
    public interface IServiceModuleRepository : IGenericRepository<ServiceModule>
    {
        Task<IEnumerable<ServiceModule>> GetOrganizationAsync(string id);

        Task<ServiceModule> GetbyIdWithDetailsAsync(string id);

        Task<ServiceModule> GetByOrganizationAndTypeAsync(string organizationType);

        Task<ServiceModule?> GetByServiceTypeAsync(string organizationId, string serviceType);

        Task<IEnumerable<ServiceModule>> GetEnabledServicesAsync(string organizationId);

        Task<bool> ServiceExistsAsync(string organizationId, string serviceType);

        Task<int> GetTotalServicesCountAsync(string organizationId);

        Task<int> GetEnabledServicesCountAsync(string organizationId);

        Task<ServiceConfiguration?> GetConfigurationAsync(string serviceModuleId);

        Task UpdateConfigurationAsync(ServiceConfiguration configuration);

        Task<ServiceStatistics?> GetStatisticsAsync(string serviceModuleId);

        Task UpdateStatisticsAsync(ServiceStatistics statistics);

        Task IncrementStatisticsAsync(string serviceModuleId, string metricName, int increment = 1);
    }
}