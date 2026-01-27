using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OrganizationCore.Email_Sender;
using OrganizationCore.Paasword;
using OrganizationCore.UnitOfWork;
using OrganizationIInterface.IReporitory;
using OrganizationIInterface.IReporitory.Assignments;
using OrganizationIInterface.IReporitory.Schools;
using OrganizationIInterface.IReporitory.Schools.Settings;
using OrganizationIInterface.IService;
using OrganizationIInterface.IService.Assignments;
using OrganizationIInterface.IService.School;
using OrganizationIInterface.IService.School.Settings;
using OrganizationModels.Model;
using OrganizationRepository;
using OrganizationRepository.Assignments;
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
            services.AddHttpClient();

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

            services.AddScoped<IRegistrationLinkRepositoryInterface, RegistrationRepository>();
            services.AddScoped<IRegistrationLinkServiceInterface, RegistrationService>();
            services.AddScoped<ICommunicationMessageInterfaceRepository, ComminicationMessageRepository>();

            services.AddScoped<IEventInterfaceRerpository, EventRepository>();
            services.AddScoped<IEventServiceInterface, EventServices>();
            services.AddScoped<IActivitiesRepositoryInertface, ActivitiesRepository>();
            services.AddScoped<IActitiesServicesInterface, ActivitiesServices>();

            services.AddScoped<IAttendanceSessionInterfaceRepository, AttendanceSessionRepository>();
            services.AddScoped<IStudentAttendanceInterfaceRepository, StudentAttendanceRepository>();
            services.AddScoped<IClassScheduleInterfaceRepository, ClassScheduleRepository>();
            services.AddScoped<ITeachingClassInterfaceRepository, TeachingClassRepository>();
            services.AddScoped<ITeacherScheduleInterfaceService, TeacherScheduleService>();

            services.AddScoped<IAssignmentRepositoryInterface, AssingmentRepository>();
            services.AddScoped<IAssignmentSubmissionRepositoryInterface, AssignmentSubmissionRepository>();
            services.AddScoped<IAssingmentGradesRepositoryInterface, AssignmentGradesRepository>();
            services.AddScoped<IAssignmentServiceInterface, AssignmentServices>();

            services.AddScoped<IAttendanceOverViewRepositoryInterface, AttendanceOverviewRepository>();
            services.AddScoped<ITeacherDashboardViewRepositoryInterface, TeacherDashboardRepository>();
            services.AddScoped<INewClassRepositoryInterface, NewClassRepository>();
            services.AddScoped<INewClassServicesInterface, NewClassServices>();

            services.AddScoped<IClassPerformanceViewRepositoryInterface, ClassPerformanceViewRepository>();
            services.AddScoped<IAttendanceDailyToMonthlyRepositoryInterface, MonthlyAttendanceSummaryRepository>();
            services.AddScoped<IPasswordChangeRepositoryInterface, ChangePasswordRepository>();

            services.AddScoped<IScheduledWorkRpositoryInterface, ScheduledWorkshopRepository>();
            services.AddScoped<IScheduledWorkshopServiceInterface, ScheduledMettingsService>();
            services.AddScoped<ILeadershipPropgramRepositoryInterface, LeadershipProgramRepository>();
            services.AddScoped<ILeadershipPropgramServirceInterface, LeadershipProgramsService>();

            return services;
        }
    }
}