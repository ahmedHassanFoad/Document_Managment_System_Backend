using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Core.Entities
{
    public class Document : BaseEntity
    {
       
        public string Name { get; set; }
        public string type { get; set; }
        public string Version { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int DirectoryId { get; set; }
        public string  path {get; set;}
        public DateTime Date {get; set;}
        public string Owner { get; set; }
        public Directory Directory { get; set; }
    }
}
