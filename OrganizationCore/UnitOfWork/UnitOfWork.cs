using OrganizationData;
using OrganizationIInterface.IReporitory;
using OrganizationIInterface.IReporitory.Assignments;
using OrganizationIInterface.IReporitory.Schools;
using OrganizationIInterface.IReporitory.Schools.Settings;

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

        public INewClassRepositoryInterface NewClass { get; private set; }

        public IClassPerformanceViewRepositoryInterface ClassPerformance { get; private set; }

        public IAttendanceDailyToMonthlyRepositoryInterface MonthlyAttendance { get; private set; }

        public IPasswordChangeRepositoryInterface ChangePassword { get; private set; }

        public IScheduledWorkRpositoryInterface ScheduledWorkshop { get; private set; }

        public ILeadershipPropgramRepositoryInterface LeadershipPropgram { get; private set; }

        public IStudentSubjectRepositoryInterface StudentSubject { get; set; }

        public IStudentScheduledTimetableRepositoryInterface StudentScheduledTimeTable { get; private set; }

        public IAcademicProgressRepositoryInterface AcademicProgress { get; private set; }

        public IStudentAttendanceOverviewRepositoryInterface StudentAttendanceOverview { get; private set; }

        public IStudentDashboardRepositoryInterface StudentDashboard { get; private set; }

        public IBackToBackCommunicationRepositoryInterface BackToBackCommunication {  get; private set; }

        public AIMarkingRequestRepositoryInterface MarkingRequest { get; private set; }

        public AIMarkingResponseRepositoryInterface MarkingResponse { get; private set; }

        public IPlagiarismRepositoryInterface PlagiarimsResults { get; set; }

        public IVideoUploadRepositoryInterface VideoUpload { get; set; }

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
                          ISchoolDashboardRepositoryInterface schoolDashboard,
                          INewClassRepositoryInterface newClass,
                          IClassPerformanceViewRepositoryInterface classPerformance,
                          IAttendanceDailyToMonthlyRepositoryInterface monthlyAttendance,
                          IPasswordChangeRepositoryInterface changePassword,
                          IScheduledWorkRpositoryInterface scheduledWorkshop,
                          ILeadershipPropgramRepositoryInterface leadershipPropgram,
                          IStudentSubjectRepositoryInterface studentSubject,
                          IStudentScheduledTimetableRepositoryInterface studentScheduledTimetable,
                          IStudentAttendanceOverviewRepositoryInterface studentAttendanceOverview,
                          IAcademicProgressRepositoryInterface academicProgress,
                          IStudentDashboardRepositoryInterface studentDashboard,
                          IBackToBackCommunicationRepositoryInterface backToBackCommunication,
                          AIMarkingResponseRepositoryInterface markingResponse,
                          AIMarkingRequestRepositoryInterface markingRequest,
                          IPlagiarismRepositoryInterface plagiarimsResults,
                          IVideoUploadRepositoryInterface videoUpload)
        {
            _Context = context;
            Users = user ?? throw new ArgumentNullException(nameof(user));
            Organization = organization ?? throw new ArgumentNullException(nameof(organization));
            Admin = admin ?? throw new ArgumentNullException(nameof(admin));
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
            NewClass = newClass ?? throw new ArgumentNullException(nameof(newClass));
            ClassPerformance = classPerformance ?? throw new ArgumentNullException(nameof(classPerformance));
            MonthlyAttendance = monthlyAttendance ?? throw new ArgumentNullException(nameof(monthlyAttendance));
            ChangePassword = changePassword ?? throw new ArgumentNullException(nameof(changePassword));
            ScheduledWorkshop = scheduledWorkshop ?? throw new ArgumentNullException(nameof(scheduledWorkshop));
            LeadershipPropgram = leadershipPropgram ?? throw new ArgumentNullException(nameof(leadershipPropgram));
            StudentSubject = studentSubject ?? throw new ArgumentNullException(nameof(studentSubject));
            StudentScheduledTimeTable = studentScheduledTimetable ?? throw new ArgumentNullException(nameof(studentScheduledTimetable));
            AcademicProgress = academicProgress ?? throw new ArgumentNullException(nameof(academicProgress));
            StudentAttendanceOverview = studentAttendanceOverview ?? throw new ArgumentNullException(nameof(studentAttendanceOverview));
            StudentDashboard = studentDashboard ?? throw new ArgumentNullException(nameof(studentDashboard));
            BackToBackCommunication = backToBackCommunication ?? throw new ArgumentNullException(nameof(backToBackCommunication));
            MarkingRequest = markingRequest ?? throw new ArgumentNullException(nameof(markingRequest));
            MarkingResponse = markingResponse ?? throw new ArgumentNullException(nameof(MarkingResponse));
            PlagiarimsResults = plagiarimsResults ?? throw new ArgumentNullException(nameof(plagiarimsResults));
            VideoUpload = videoUpload ?? throw new ArgumentNullException(nameof(videoUpload));
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
