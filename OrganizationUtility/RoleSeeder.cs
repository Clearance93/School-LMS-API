using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace OrganizationUtility
{
    public static class RoleSeeder
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager, ILogger? logger = null)
        {
            var roles = new[]
            {
                RoleNames.Admin,
                RoleNames.Teacher,
                RoleNames.Student,
                RoleNames.Learner,
                RoleNames.StuffMember,
                RoleNames.Guest
            };

            foreach (var role in roles)
            {
                logger?.LogInformation($"Checking if role '{role}' exists...");

                if (!await roleManager.RoleExistsAsync(role))
                {
                    logger?.LogInformation($"Creating role '{role}'...");
                    var result = await roleManager.CreateAsync(new IdentityRole(role));

                    if (result.Succeeded)
                    {
                        logger?.LogInformation($"Role '{role}' created successfully.");
                    }
                    else
                    {
                        logger?.LogError($"Failed to create role '{role}': {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    }
                }
                else
                {
                    logger?.LogInformation($"Role '{role}' already exists.");
                }
            }
        }
    }
}