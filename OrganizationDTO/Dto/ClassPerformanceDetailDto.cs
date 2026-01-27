namespace OrganizationDTO.Dto
{
    public class ClassPerformanceDetailDto
    {
        public Guid StreamId { get; set; }

        public Guid GradeId { get; set; }

        public Guid OrganizationId { get; set; }

        public Guid? TeacherId { get; set; }

        public string? ClassName { get; set; }

        public decimal AveragePerformance { get; set; }

        public int TotalStudents { get; set; }

        public int ActiveStudents { get; set; }

        public int TotalAssignments { get; set; }

        public int UpcomingAssignments { get; set; }

        public int PastDueAssignments { get; set; }

        public int TotalSubmissions { get; set; }

        public int TotalGradedSubmissions { get; set; }

        public int RecentlyGraded { get; set; }

        public decimal CompletionRate { get; set; }

        public decimal HighestPerformance { get; set; }

        public decimal LowestPerformance { get; set; }

        public decimal AverageMarksEarned { get; set; }

        public decimal AverageMarksTotal { get; set; }

        public DateTime? LastGradedDate { get; set; }

        public DateTime? FirstAssignmentDate { get; set; }

        public DateTime? NextDueDate { get; set; }

        public DateTime StreamCreatedDate { get; set; }

        public DateTime StreamUpdatedDate { get; set; }
    }
}
