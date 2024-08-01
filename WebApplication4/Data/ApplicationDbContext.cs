using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Models;

namespace WebApplication4.Data
{
    public class ApplicationDbContext:IdentityDbContext
    {
        internal object products;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
            {

            }
        public DbSet<Product> Products { get; set; }
		public DbSet<Contact> Contacts { get; set; }
        public DbSet<Carty> carts { get; set; }
        public DbSet<Blogy> blogs { get; set; }
	}
}
