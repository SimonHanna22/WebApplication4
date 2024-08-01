using WebApplication4.Models;

namespace WebApplication4.ViewModel
{
    public class ShopViewModel
    {
        public List<Product> prod { get; set; }
        public int TotalNumberOfPages { get; set; }

        public int CurrentPage { get; set; }
    }
}
