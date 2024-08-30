using System.ComponentModel.DataAnnotations;

namespace DMS.APIs.Dto
{
    public class WorSpaceDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
