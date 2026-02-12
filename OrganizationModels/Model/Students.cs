using OrganizationModels.Model.Settings;
using System.ComponentModel.DataAnnotations;

namespace OrganizationModels.Model
{
    public class Students
    {
        [Key]
        public Guid StudentId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? StudentEmail { get; set; }

        public string? StudentProfilePicture { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? Gender { get; set; }

        public string? StudentAddress { get; set; }

        public string? StudentEmergencyContact { get; set; }

        public string? StudentContactNumber { get; set; }

        public bool? IsDeleted { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public Guid? GradeStreamId { get; set; }
        public GradeStream? GradeStream { get; set; }

        public Guid? OrganizationSetupId { get; set; }

        public OrganizationSetup? OrganizationSetup { get; set; }
    }
}
