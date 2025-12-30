using System.ComponentModel.DataAnnotations;

namespace OrganizationStatistics
{
    public class GradebookStatistics
    {
        public int TotalGradesEntered { get; set; }

        public int TotalReportsGenerated { get; set; }

        [Range(0, 100)]
        public decimal AverageClassPerfomance { get; set; }

        public int PendingGrades {  get; set; }
    }
}
