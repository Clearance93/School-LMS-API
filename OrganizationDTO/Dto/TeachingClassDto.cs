namespace OrganizationDTO.Dto
{
    public class TeachingClassDto
    {
        public Guid TeachingClassId { get; set; }

        public Guid GradeStreamId { get; set; }

        public string? Subject { get; set; }

        public int TotalStudents { get; set; }

        public string? ClassRoomNumber { get; set; }

        public string? GradeStreamName {  get; set; }

        public Guid OrganizationId { get; set; }

        public Guid TeacherId { get; set; }
    }
}
