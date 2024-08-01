using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication4.Models
{
    public class Blogy
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        [Key]
        public int BlogId { get; set;}
        public DateTime BlogDate { get; set; }
        [NotMapped]
        public IFormFile BlogImage { get; set; }
        public string? ImagesPath { get; set; }
    }
}
