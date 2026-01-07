namespace OrganizationDTO.Dto
{
    public class StudentAttendanceDto
    {
        public Guid StudentAttendanceId { get; set; }

        public Guid AttendanceSessionId { get; set; }

        public Guid StudentId { get; set; }

        public bool IsPresent { get; set; }

        public Guid OrganizationId { get; set; }

        public Guid TeacherId { get; set; }
    }
}
