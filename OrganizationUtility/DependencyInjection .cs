using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OrganizationCore.Email_Sender;
using OrganizationCore.UnitOfWork;
using OrganizationIInterface.IReporitory;
using OrganizationIInterface.IService;
using OrganizationRepository;
using OrganizationServices;
using Services.Repository;

namespace OrganizationUtility
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services,
                                                                         IConfiguration configuration)
        {
            services.AddSingleton(provider =>
            {
                var loggerFactory = provider.GetRequiredService<ILoggerFactory>();
                var configExpression = new MapperConfigurationExpression();
                configExpression.AddProfile<OrganizationCore.MappingProfile>();

                var configuration = new MapperConfiguration(configExpression, loggerFactory);
                return configuration;
            });

            services.AddSingleton<IMapper>(provider =>
            {
                var configuration = provider.GetRequiredService<MapperConfiguration>();
                return new Mapper(configuration);
            });

            services.AddSingleton<IEmailSender>(provider =>
                new EmailSender(
                        smtpServer: configuration["EmailSettings:SmtpServer"],
                        smtpPort: int.Parse(configuration["EmailSettings:SmtpPort"]),
                        fromEmail: configuration["EmailSettings:FromEmail"],
                        smtpUser: configuration["EmailSettings:SmtpUser"],
                        smtpPass: configuration["EmailSettings:SmtpPass"]
                    ));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUSerRepositoryInterface, UserRepository>();
            services.AddScoped<IUserServiceInterface, UserService>();
            services.AddScoped<IUserEmailSenderInterface, UserEmailSender>();

            services.AddScoped<IOrganizationRepositoryInterface, OrganizationRepositories>(); 
            services.AddScoped< IOrganizationServiceInterface, OrganizationService>();
            services.AddScoped<IAdminServiceInterface, AdminService>();
            services.AddScoped<IAdminRepositoryInterface, AdminRepository>();

            return services;
        }
    }
}