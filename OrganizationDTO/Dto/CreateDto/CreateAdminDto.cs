namespace OrganizationDTO.Dto.CreateDto
{
    public class CreateAdminDto
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? AdminBusinessEmail { get; set; }

        public string? AdminProfilePicture { get; set; }

        public bool IsDeleted { get; set; }

        public Guid? RegistrationLink { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsSuperAdmin { get; set; }

        public Guid OrganizationSetupId { get; set; }
    }
}
