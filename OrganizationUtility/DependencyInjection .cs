using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OrganizationCore.Email_Sender;
using OrganizationCore.Paasword;
using OrganizationCore.UnitOfWork;
using OrganizationIInterface.IReporitory;
using OrganizationIInterface.IReporitory.Schools;
using OrganizationIInterface.IReporitory.Schools.Settings;
using OrganizationIInterface.IService;
using OrganizationIInterface.IService.School;
using OrganizationIInterface.IService.School.Settings;
using OrganizationRepository;
using OrganizationRepository.Schools;
using OrganizationRepository.Settings;
using OrganizationServices;
using OrganizationServices.School;
using OrganizationServices.School.Settings;
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

            services.AddScoped<IGuestsRepositoryInterface, GuestRepositry>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ILearnerrepositoryInterface, LearnersRepository>();
            services.AddScoped<IStuffMemberRepositoryInterface,StuffMemberRepository>();

            services.AddScoped<IGuestServiceInterface, GuestService>();
            services.AddScoped<ITeacherServiceInterface, TeacherService>();
            services.AddScoped<ILearnerServiceInterface, LearnerService>();
            services.AddScoped<IStuffMemberServiceInterface, StuffMemberService>();
            services.AddScoped<IStudentServiceInterface, StudentServices>();

            services.AddScoped<ISchoolDashboardRepositoryInterface, AdminDashboardRepository>();
            services.AddScoped<ISchoolDashboardServiceInterface, SchoolDashboardServices>();
            services.AddScoped<IPasswordGenerationInterface, PasswordGeneration>();

            services.AddScoped<ISchoolAdminSettingsrepositoryInterface, SchoolAdminSettingsRepository>();
            services.AddScoped<ISchoolAdminSettingsServiceInterface, SchoolAdminSettingsServices>();

            services.AddScoped<IGradeStreamRepositoryInterface, GradeStreamRepository>();
            services.AddScoped<IGradeRepositoryInterface, GradeRepository>();
            services.AddScoped<ISettingsServiceInterface, SettingsService>();

            services.AddScoped<ICourseStreamRepositoryInterface, CourseStreamRepository>();
            services.AddScoped<ISchoolSubjectRepositoryInterface, SchoolSubjectRepository>();
            services.AddScoped<ICourseStreamServiceInterface, CourseStreamService>();
            services.AddScoped<ISchoolSubjectServiceInterface, SchoolSubjectService>();

            services.AddScoped<IExamGradeScaleRepositoryInterface, ExamGradeScaleRepository>();
            services.AddScoped<IExamGradeScaleServiceInterface, ExamGradeScaleService>();
            services.AddScoped<ILibraryInterfaceRepository, LibraryRepository>();
            services.AddScoped<ILibraryInterfaceService, LibrarService>();

            return services;
        }
    }
}