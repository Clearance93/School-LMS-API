namespace OrganizationDTO.Dto
{
    public class PreRecordedVideoDto
    {
        public Guid PreRecordedVideoId { get; set; }

        public Guid TeacherId { get; set; }

        public Guid GradeStreamId { get; set; }

        public string? TeacherFullNames { get; set; }

        public string? StreamName { get; set; }

        public string? VideoTitle { get; set; }

        public string? Description { get; set; }

        public string? VideoUpload { get; set; }

        public DateTime UploadedTime { get; set; }
    }
}