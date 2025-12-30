using System.ComponentModel.DataAnnotations;

namespace OrganizationStatistics
{
    public class AttendanceStatistics
    {
        public int TotalAttendancerecords { get; set; }

        [Range(0, 100)]
        public decimal AverageAttendanceRecords { get; set; }

        public int AbsentToday { get; set; }

        public int LateToday { get; set; }

        public int PresentToday { get; set; }
    }
}
