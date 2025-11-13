using System.Diagnostics;

namespace OrganizationDTO.Dto.Settings
{
    public class StreamGradeDto
    {
        public Guid StreamId { get; set; }

        public Guid TeacherId { get; set; }

        public Guid GradeId { get; set; }

        public string? StreamName { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
