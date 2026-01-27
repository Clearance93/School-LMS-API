namespace OrganizationDTO.Dto
{
    public class MonthlyAttendanceDto
    {
        public Guid OrganizationId { get; set; }

        public Guid TeacherId { get; set; }

        public Guid GradeStreamId { get; set; }

        public Guid TeachingClassId { get; set; }

        public string? Subject { get; set; }

        public string? ClassRoomNumber { get; set; }

        public string? ClassName { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public string? MonthName { get; set; }

        public DateTime MonthStartDate { get; set; }

        public DateTime MonthEndDate { get; set; }

        public int ActualStudentsInGrade { get; set; }

        public int ActiveStudentsInGrade { get; set; }

        public int ManualTotalStudents { get; set; }

        public int TotalStudentsForCalculation { get; set; }

        public string? StudentCountSource { get; set; } 

        public int MonthlyTotalPresent { get; set; }

        public int MonthlyTotalAbsent { get; set; }

        public int DaysRecorded { get; set; }

        public int MonthlyPossibleAttendance { get; set; }

        public decimal AvgDailyPresent { get; set; }

        public decimal AvgDailyAbsent { get; set; }

        public decimal MonthlyAttendancePercentage { get; set; }

        public decimal MonthlyAttendancePercentageSubmitted { get; set; }

        public int WeeksWithAttendance { get; set; }

        public int? StudentCountDiscrepancy { get; set; }

        public int AttendanceDiscrepancy { get; set; }

        public DateTime? LastSubmissionDate { get; set; }
    }
}
