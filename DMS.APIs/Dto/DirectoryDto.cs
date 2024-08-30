using System.ComponentModel.DataAnnotations;

namespace DMS.APIs.Dto
{
    public class DirectoryDto
    {
       public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters.")]

        public string name { get; set; }
       
    
        public int WorkSpaceId { get; set; }
        
    }
}
