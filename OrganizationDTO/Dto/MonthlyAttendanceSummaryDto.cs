namespace OrganizationDTO.Dto
{
    public class MonthlyAttendanceSummaryDto
    {
        public Guid TeachingClassId { get; set; }

        public string? ClassName { get; set; }

        public string? Subject { get; set; }

        public string? ClassRoomNumber { get; set; }

        public int Month { get; set; }

        public string? MonthName { get; set; }

        public DateTime MonthStartDate { get; set; }

        public DateTime MonthEndDate { get; set; }

        public int TotalStudents { get; set; }

        public int TotalPresent { get; set; }

        public int TotalAbsent { get; set; }

        public int DaysRecorded { get; set; }

        public decimal AttendanceRate { get; set; }

        public string? DataSource { get; set; } 

        public bool HasDiscrepancy { get; set; }
    }
}
