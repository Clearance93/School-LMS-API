namespace OrganizationDTO.Dto
{
    public class SchoolDashboardStatsDto
    {
        public Guid OrganizationSetupId { get; set; }

        public string? OrganizationName { get; set; }

        public int TotalAdmins { get; set; }

        public int TotalTeachers { get; set; }

        public int TotalStudents { get; set; }

        public int TotalStaff { get; set; }

        public int TotalCourseStreams { get; set; }

        public int TotalGuests { get; set; }   
        
        public string? TypeOfService { get; set; }

        public Guid AdminId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }
        
        public string? AdminBusinessEmail { get; set; }

        public string? AdminProfilePicture { get; set; }

        public bool IsSuperAdmin { get; set; }
    }
}
