using DMS.Core.Entities;

namespace DMS.APIs.Dto
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string NID { get; set; }
        public string PhoneNumber { get; set; }
        public int workSpaceID {  get; set; }
        public string Token { get; set; }
    }
}
