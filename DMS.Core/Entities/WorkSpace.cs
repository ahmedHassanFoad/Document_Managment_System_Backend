using DMS.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Core.Entities
{
    public class WorkSpace : BaseEntity
    {
       
        public string Name { get; set; }

        public AppUser user { get; set; }
        public List<Directory>? Directories { get; set; }

    }
}
