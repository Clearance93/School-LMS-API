using System.ComponentModel.DataAnnotations;

namespace OrganizationModels.Model
{
    public class PlagiarismResults
    {
        [Key]
        public Guid PlagiarismResultsId { get; set; }

        public Guid AssignmentId { get; set; }

        public Guid StudentId { get; set; }

        public double PlagiarismPercentage { get; set; }

        public double AIGeneratedPercentage { get; set; }

        public double OriginalWorkPercentage { get; set; }

        public string? PlagiarismSummary { get; set; }

        public string? AIDetectionSummary { get; set; }

        public string? OverallVerdict {  get; set; } 

        public string? MatchedSourceJson { get; set; }

        public DateTime DetectedAt { get; set; }

        public Guid? MarkingResponseId { get; set; }
    }
}
