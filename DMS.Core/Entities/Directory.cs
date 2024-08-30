using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Core.Entities
{
    public class Directory: BaseEntity 

    {
       
        public string Name { get; set; }
        
        public int WorkSpaceId { get; set; }
        public WorkSpace WorkSpace { get; set; }
        public List<Document>? Documents { get; set; }
        public bool IsDeleted { get; set; } = false;// For soft delete
        public bool IsPublic { get; set; } = false; // Default to private
    }
}
