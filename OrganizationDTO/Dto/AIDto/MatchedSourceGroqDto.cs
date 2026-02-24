namespace OrganizationDTO.Dto.AIDto
{
    public class MatchedSourceGroqDto
    {
        public string? sourceTitle { get; set; }

        public string? sourceUrl { get; set; }

        public double matchedPercentage { get; set; }

        public string? matchedText { get; set; }
    }
}
