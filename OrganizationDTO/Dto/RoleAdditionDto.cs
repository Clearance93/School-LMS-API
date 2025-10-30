namespace OrganizationDTO.Dto
{
    public class RoleAdditionDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }    

        public string Email { get; set; }

        public string ProfilePicture {  get; set; }

        public Guid OrganizationId { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string StuffMemberPosition { get; set; }
    }
}
