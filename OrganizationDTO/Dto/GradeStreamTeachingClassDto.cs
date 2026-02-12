namespace OrganizationDTO.Dto
{
    public class GradeStreamTeachingClassDto
    {
        public Guid GradeId { get; set; }

        public Guid StreamId { get; set; }

        public Guid OrganizationId { get; set; }

        public Guid StreamTeacherId { get; set; }

        public Guid TeachingClassId {  get; set; }

        public Guid SubjectTeacherId { get; set; }

        public string? Subject { get; set; }

        public string? StreamName { get; set; }

        public int TotalStudents { get; set; }

        public string? ClassRoomNumber { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? TeacherProfilePicture { get; set; }

        public DateTime StreamCreatedAt { get; set; }
    }
}
