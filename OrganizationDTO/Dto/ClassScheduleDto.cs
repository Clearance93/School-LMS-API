using OrganizationEnums;

namespace OrganizationDTO.Dto
{
    public class ClassScheduleDto
    {
        public Guid ClassScheduleId { get; set; }

        public DateOnly Date { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }

        public Guid GradeStreamId { get; set; }

        public ScheduleStatus Status { get; set; }

        public Guid TeachingClassId { get; set; }

        public Guid OrganizationId { get; set; }

        public Guid TeacherId { get; set; }
    }
}
