using OrganizationModels.Model.Settings;
using System.ComponentModel.DataAnnotations;

namespace OrganizationModels.Model
{
    public class TeachingClass
    {
        [Key]
        public Guid TeachingClassId { get; set; }

        public Guid GradeStreamId { get; set; }
        public GradeStream? GradeStream { get; set; }

        public string? Subject { get; set; }

        public int TotalStudents { get; set; }

        public Guid OrganizationId { get; set; }

        public Guid TeacherId {  get; set; }

        public ICollection<ClassSchedule>? ClassSchedule {  get; set; }
    }
}
