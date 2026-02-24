namespace OrganizationDTO.Dto.AIDto
{
    public class PlagiarismAnalysisResponseDto
    {
        public double plagiarismPercentage { get; set; }

        public double aiGeneratedPercentage { get; set; }

        public double originalWorkPercentage { get; set; }

        public string? plagiarismSummary { get; set; }

        public string? aiDetectionSummary { get; set; }

        public string? overallVerdict { get; set; }

        public List<MatchedSourceGroqDto>? matchedSources { get; set; }
    }
}
