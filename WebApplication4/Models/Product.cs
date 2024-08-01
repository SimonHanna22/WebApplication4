using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication4.Models
{
    public class Product
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-z' '-'\s]{1,40}$" , ErrorMessage ="Characters Are Not allowed.")]
        public string Name { get; set; }
        [Required]
        [Length(5,1000)]
        public string Description { get; set; }
        [Required]
        [Range(0,1000000 , ErrorMessage="Please Enter Price Starting From 0 UpTo 1000000.") ]
        public float Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        [NotMapped]
        public IFormFile ProductImage { get; set; }
        public string? ImagePath { get; set; }
    }
}
