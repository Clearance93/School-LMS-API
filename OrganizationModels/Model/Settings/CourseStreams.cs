using System.ComponentModel.DataAnnotations;

namespace OrganizationModels.Model.Settings
{
    public class CourseStreams
    {
        [Key]
        public Guid CourseStreamId { get; set; }

        public Guid OrganizationId { get; set; }
        public OrganizationSetup? Organization {  get; set; }

        public string? CourseStreamName {  get; set; }

        public string? Description { get; set; }
    }
}
