using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;
using WebApplication4.Data;
using WebApplication4.Models;
using WebApplication4.ViewModel;

namespace WebApplication4.Controllers
{
    public class Shopy(ApplicationDbContext _db):Controller
    {
     public IActionResult Shop(int? Page)
        {
            if (Page == null)
            {
                Page = 1;
            }
            int SkipNumberProduct = (int)(Page - 1) * 8;
            var pros = _db.Products.Skip(SkipNumberProduct).Take(8).ToList();
            var AllProduct = new ShopViewModel()
            {
                prod = pros,
                CurrentPage = (int)Page,
                TotalNumberOfPages = (int)Math.Ceiling(_db.Products.Count() / 8.0),

            };
            return View(AllProduct);
        }
		public IActionResult AboutUs()
		{
			return View();
		}
		public IActionResult Service()
		{
			return View();
		}
        
		public IActionResult Blog()
		{
            List<Blogy> bl=_db.blogs.ToList();
			return View(bl);
		}
        public IActionResult Blogss(int bid)
        {
            Blogy blog=_db.blogs.FirstOrDefault(m=>m.BlogId == bid);
            
            return View(blog);
        }
        public IActionResult Contactt()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Contactt([FromForm]Contact Model)
		{
			
			_db.Contacts.Add(Model);
			_db.SaveChanges();
			return RedirectToAction("Shop");
		}
        #region cart

        public IActionResult Cart(int pid)
		{
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); //find someone that login now
            var CartProduct = _db.carts.Include(m => m.Product).Where(c => c.UserId == userId).ToList();


            return View(CartProduct);
		}
		public IActionResult AddToCart(int pid)
		{

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var CartProduct = _db.carts.Where(c => c.UserId == userId).SingleOrDefault(m => m.ProductId == pid);
            if (CartProduct == null)
            {
              //  var product = _db.Products.SingleOrDefault(p => p.Id == pid);
                var saveCartProoduct = new Carty()
                {
                    ProductId = pid,
                    Quantity = 1,
                    UserId = userId
                };
                _db.carts.Add(saveCartProoduct);
            }
            else
            {
                CartProduct.Quantity += 1;
            }
            _db.SaveChanges();
            return RedirectToAction("Cart");
		}
      public IActionResult RemoveFromCart(int pid)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var deletedProduct = _db.carts.Where(c => c.UserId == userId).SingleOrDefault(m => m.ProductId == pid);
            if(deletedProduct != null)
            {
                _db.carts.Remove(deletedProduct);
            }
            _db.SaveChanges();
            return RedirectToAction("Cart");

        }
        public IActionResult CartIncQuant(int quant, int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            var cartProduct = _db.carts.Where(c => c.UserId == userId).SingleOrDefault(c => c.Id == id);

            if (quant > 0)
            {
                cartProduct.Quantity = quant;

            }
            else
            {
                _db.carts.Remove(cartProduct);
            }


            cartProduct.Quantity = quant;
            _db.SaveChanges();
            return RedirectToAction("Cart");
        }
        #endregion
        public IActionResult CheckOut(int cid) {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); //find someone that login now
            var CartProduct = _db.carts.Include(m => m.Product).Where(c => c.UserId == userId).ToList();
            


            return View(CartProduct);
        }
       
    }
}
