namespace DMS.APIs.Dto
{
    public class UserDetailsDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string NID { get; set; }
        public WorSpaceDto WorkSpace { get; set; }
    }
}
