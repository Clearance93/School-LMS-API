using Microsoft.EntityFrameworkCore;
using OrganizationData;
using OrganizationIInterface.IReporitory;
using OrganizationModels.Model.Settings;

namespace OrganizationRepository
{
    public class SchoolAdminSettingsRepository : GenericRepository<SchoolAdminSettings>, ISchoolAdminSettingsrepositoryInterface
    {
        private readonly ApplicationDbContext _Context;
        public SchoolAdminSettingsRepository(ApplicationDbContext context) : base(context)
        {
            _Context = context;
        }

        public async Task<SchoolAdminSettings?> GetSchoolAdminSettingsByIsAsync(Guid id)
        {
            return await _Context.SchoolAdminSettings.FirstOrDefaultAsync(s => s.OrganizationId == id);
        }
    }
}
