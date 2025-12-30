namespace OrganizationDTO.Dto.Settings
{
    public class GradeDto
    {
        public Guid GradeId { get; set; }

        public string? GradeName { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
