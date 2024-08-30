using DMS.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace DMS.APIs.Dto
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression("(?=^.{6,10}$)(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[!@#$%^&amp;*()_+]).*$",
          ErrorMessage = "Password Must Contain 1 UpperCase ,1 LowerCase")]
        public string Password { get; set; }
        

        [Required]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^\d{14}$", ErrorMessage = "NID must be exactly 14 Number.")]
        public string NID { get; set; }

        [Required]
        [RegularExpression(@"^\d{12}$", ErrorMessage = "Phone number must be exactly 12 Number.")]
        public string PhoneNumber { get; set; }
        [Required]
        public string WorkSpaceName { get; set; }
    }
}
