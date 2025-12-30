using OrganizationModels.Model.Settings;

namespace OrganizationIInterface.IReporitory
{
    public interface ISchoolAdminSettingsrepositoryInterface : IGenericRepository<SchoolAdminSettings>
    {
        Task<SchoolAdminSettings?> GetSchoolAdminSettingsByIsAsync(Guid id);
    }
}
