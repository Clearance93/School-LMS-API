using OrganizationData;
using OrganizationIInterface.IReporitory;
using OrganizationIInterface.IReporitory.Assignments;
using OrganizationIInterface.IReporitory.Schools;
using OrganizationIInterface.IReporitory.Schools.Settings;
using OrganizationIInterface.IService;

namespace OrganizationCore.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _Context;
        public IUSerRepositoryInterface Users { get; private set; }

        public IOrganizationRepositoryInterface Organization { get; private set; }

        public IAdminRepositoryInterface Admin {get; private set; }

        public ILearnerrepositoryInterface Learner { get; private set; }

        public IStudentRepository Student { get; private set; }

        public IGuestsRepositoryInterface Guests { get; private set; }

        public ITeacherRepository Teacher { get; private set; }

        public IStuffMemberRepositoryInterface StuffMember { get; private set; }

        public ISchoolAdminSettingsrepositoryInterface SchoolAdminSettings {  get; private set; }

        public IGradeRepositoryInterface Grade { get; private set; }

        public IGradeStreamRepositoryInterface GradeStream {  get; private set; }

        public ICourseStreamRepositoryInterface CourseStream { get; private set; }

        public ISchoolSubjectRepositoryInterface SchoolSubject { get; private set; }

        public IExamGradeScaleRepositoryInterface ExamGrade { get; private set; }

        public ILibraryInterfaceRepository Library { get; private set; }

        public IRegistrationLinkRepositoryInterface RegistrationLink { get; private set; }

        public ICommunicationMessageInterfaceRepository Communication { get; private set; }

        public IEventInterfaceRerpository Events {  get; private set; }

        public IActivitiesRepositoryInertface Activities {  get; private set; }

        public IClassScheduleInterfaceRepository ClassSchedule { get; private set; }

        public IStudentAttendanceInterfaceRepository StudentAttendance { get; private set; }

        public IAttendanceSessionInterfaceRepository AttendanceSession { get; private set; }

        public ITeachingClassInterfaceRepository TeachingClass { get; private set; }

        public IAssingmentGradesRepositoryInterface AssignmentGrades { get; private set; }

        public IAssignmentSubmissionRepositoryInterface AssignmentSubmission { get; private set; }

        public IAssignmentRepositoryInterface Assignments { get; private set; }

        public IAttendanceOverViewRepositoryInterface AttendanceOverview { get; private set; }

        public ITeacherDashboardViewRepositoryInterface TeacherDashboard {get ; private set;}

        public ISchoolDashboardRepositoryInterface SchoolDashboard { get; private set; }

        public UnitOfWork(ApplicationDbContext context,
                          IUSerRepositoryInterface user,
                          IOrganizationRepositoryInterface organization,
                          IAdminRepositoryInterface admin,
                          ILearnerrepositoryInterface learner,
                          ITeacherRepository teacher,
                          IGuestsRepositoryInterface guest,
                          IStuffMemberRepositoryInterface stuffMember,
                          IStudentRepository student,
                          ISchoolAdminSettingsrepositoryInterface schoolAdminSettings,
                          IGradeRepositoryInterface grade,
                          IGradeStreamRepositoryInterface gradeStream,
                          ISchoolSubjectRepositoryInterface schoolSubject,
                          ICourseStreamRepositoryInterface courseStream,
                          IExamGradeScaleRepositoryInterface examGrades,
                          ILibraryInterfaceRepository libary,
                          IRegistrationLinkRepositoryInterface registrationLink,
                          ICommunicationMessageInterfaceRepository communication,
                          IEventInterfaceRerpository events,
                          IActivitiesRepositoryInertface activities,
                          ITeachingClassInterfaceRepository teachingClass,
                          IAttendanceSessionInterfaceRepository attendenceSession,
                          IStudentAttendanceInterfaceRepository studentAttendance,
                          IClassScheduleInterfaceRepository classSchedule,
                          IAssingmentGradesRepositoryInterface assignmentGrades,
                          IAssignmentSubmissionRepositoryInterface assignmentSubmission,
                          IAssignmentRepositoryInterface assignment,
                          IAttendanceOverViewRepositoryInterface attendanceOverview,
                          ITeacherDashboardViewRepositoryInterface teacherDashboard,
                          ISchoolDashboardRepositoryInterface schoolDashboard)
        {
            _Context = context;
            Users = user ?? throw new ArgumentNullException(nameof(user));
            Organization = organization ?? throw new ArgumentNullException(nameof(organization));
            Admin = admin ?? throw new ArgumentNullException(nameof(organization));
            Student = student ?? throw new ArgumentNullException(nameof(student));
            Learner = learner ?? throw new ArgumentNullException(nameof(learner));
            StuffMember = stuffMember ?? throw new ArgumentNullException(nameof(stuffMember));
            Guests = guest ?? throw new ArgumentNullException(nameof(guest));
            Teacher = teacher ?? throw new ArgumentNullException(nameof(teacher));
            SchoolAdminSettings = schoolAdminSettings ?? throw new ArgumentNullException(nameof(schoolAdminSettings));
            Grade = grade ?? throw new ArgumentNullException(nameof(grade));
            GradeStream = gradeStream ?? throw new ArgumentNullException(nameof(gradeStream));
            SchoolSubject = schoolSubject ?? throw new ArgumentNullException(nameof(schoolSubject));
            CourseStream = courseStream ?? throw new ArgumentNullException(nameof(courseStream));
            ExamGrade = examGrades ?? throw new ArgumentNullException(nameof(examGrades));
            Library = libary ?? throw new ArgumentNullException(nameof(libary));
            RegistrationLink = registrationLink ?? throw new ArgumentNullException(nameof(registrationLink));
            Communication = communication ?? throw new ArgumentNullException(nameof(communication));
            Events = events ?? throw new ArgumentNullException(nameof(events));
            Activities = activities ?? throw new ArgumentNullException(nameof(activities));
            ClassSchedule = classSchedule ?? throw new ArgumentNullException(nameof(classSchedule));
            TeachingClass = teachingClass ?? throw new ArgumentNullException(nameof(teachingClass));
            StudentAttendance = studentAttendance ?? throw new ArgumentNullException(nameof(studentAttendance));
            AttendanceSession = attendenceSession ?? throw new ArgumentNullException(nameof(attendenceSession));
            AssignmentGrades = assignmentGrades ?? throw new ArgumentNullException(nameof(AssignmentGrades));
            AssignmentSubmission = assignmentSubmission ?? throw new ArgumentNullException(nameof(assignmentSubmission));
            Assignments = assignment ?? throw new ArgumentNullException(nameof(assignment));
            AttendanceOverview = attendanceOverview ?? throw new ArgumentNullException(nameof(attendanceOverview));
            TeacherDashboard = teacherDashboard ?? throw new ArgumentNullException(nameof(teacherDashboard));
            SchoolDashboard = schoolDashboard ?? throw new ArgumentNullException(nameof(schoolDashboard));
        }

        public void Dispose()
        {
            _Context.Dispose();
        }

        public async Task<int> SaveChangeAsync()
        {
            return await _Context.SaveChangesAsync();
        }
    }
}
