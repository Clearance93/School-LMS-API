namespace OrganizationDTO.Dto
{
    public class WeeklyAttendanceDto
    {
        public Guid OrganizationId { get; set; }
        public Guid TeacherId { get; set; }

        public Guid GradeStreamId { get; set; }

        public Guid TeachingClassId { get; set; }

        public string? Subject { get; set; }

        public string? ClassRoomNumber { get; set; }

        public string? ClassName { get; set; }

        public int Year { get; set; }

        public int WeekNumber { get; set; }

        public DateTime WeekStartDate { get; set; }

        public DateTime WeekEndDate { get; set; }

        public int ActualStudentsInGrade { get; set; }

        public int ActiveStudentsInGrade { get; set; }

        public int ManualTotalStudents { get; set; }

        public int TotalStudentsForCalculation { get; set; }

        public string? StudentCountSource { get; set; } 

        public int WeeklyTotalPresent { get; set; }

        public int WeeklyTotalAbsent { get; set; }

        public int DaysRecorded { get; set; }

        public int WeeklyPossibleAttendance { get; set; }

        public decimal AvgDailyPresent { get; set; }

        public decimal AvgDailyAbsent { get; set; }

        public decimal WeeklyAttendancePercentage { get; set; }

        public decimal WeeklyAttendancePercentageSubmitted { get; set; }

        public int? StudentCountDiscrepancy { get; set; }

        public int AttendanceDiscrepancy { get; set; }

        public DateTime? LastSubmissionDate { get; set; }
    }
}
