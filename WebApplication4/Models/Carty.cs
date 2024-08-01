using Microsoft.AspNetCore.Identity;

namespace WebApplication4.Models
{
    public class Carty
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
       
        public int Quantity { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}
