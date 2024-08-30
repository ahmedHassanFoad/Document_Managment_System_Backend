using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Core.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }

        [RegularExpression(@"^\d{14}$", ErrorMessage = "NID must be exactly 14 Number.")]
        public string NID { get; set; }

        [RegularExpression(@"^\d{12}$", ErrorMessage = "Phone number must be exactly 12 Number.")]
        public string PhoneNumber { get; set; }

        public int WorkSpaceID { get; set; }
        public WorkSpace WorkSpace { get; set; }


    }
}
