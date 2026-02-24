namespace OrganizationDTO.Dto.AIDto
{
    public class MatchedSourceDto
    {
        public string? SourceTitle { get; set; }

        public string? SourceUrl { get; set; }

        public double MatchPercentage { get; set; }

        public string? MatchedText { get; set; }
    }
}
