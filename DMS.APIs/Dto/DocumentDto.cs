using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace DMS.APIs.Dto
{
    public class DocumentDto
    {
        public string Name {  get; set; }
        public String version { get; set; }
        public string Type { get; set; }
        public string path { get; set; }
        public DateTime date { get; set; }
    }
}
