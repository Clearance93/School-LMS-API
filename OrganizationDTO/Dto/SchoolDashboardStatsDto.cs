namespace OrganizationDTO.Dto
{
    public class SchoolDashboardStatsDto
    {
        public Guid OrganizationSetupId { get; set; }

        public string? OrganizationSetupName { get; set; }

        public int TotalAdmins { get; set; }

        public int TotalTeachers { get; set; }

        public int TotalStudents { get; set; }

        public int TotalStuff { get; set; }

        public int TotalGuests { get; set; }    
    }
}
