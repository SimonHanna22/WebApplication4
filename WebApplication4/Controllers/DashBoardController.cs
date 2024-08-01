using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using WebApplication4.Data;
using WebApplication4.Models;
using WebApplication4.Utility;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace WebApplication4.Controllers
{
    [Authorize]
    public class DashBoardController : Controller
    {
        //private static List<Product> products=new List<Product>();
        private readonly ApplicationDbContext _db;
        private readonly IHostingEnvironment host;
        public DashBoardController(ApplicationDbContext db,IHostingEnvironment host)
        {
            _db = db;
            this.host = host;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles =RL.RoleAdmin)]
        public IActionResult AddProduct()
        {
            
            return View();

        }
        [HttpPost]
		[Authorize(Roles = RL.RoleAdmin)]
		public IActionResult AddProduct(Product product)
        {
            if (product.ProductImage != null)
            {
                string ImageFolder = Path.Combine(host.WebRootPath, "images");
                string Imagepath = Path.Combine(ImageFolder, product.ProductImage.FileName);
                product.ProductImage.CopyTo(new FileStream(Imagepath, FileMode.Create));
                product.ImagePath = product.ProductImage.FileName;
            }

            if (!ModelState.IsValid) {
               
                int id;
                if (_db.Products.Count() == 0)
                    id = 0;
                else
                    id = _db.Products.Max(x => x.Id) + 1;
                product.Id = id;
                return View(product);
            }
            _db.Products.Add(product);
            _db.SaveChanges();
            
            return RedirectToAction("index");
        }
        #region
        
        public IActionResult GetAllData()
        {
           
    
            return View(_db.Products);

        }
        #endregion
        #region delete
        public IActionResult Delete(int id)
        {
            Product prod = _db.Products.FirstOrDefault(x => x.Id == id); //get id i wanna delete
           _db.Products.Remove(prod);
            _db.SaveChanges();
            return RedirectToAction("GetAllData");
        }
		#endregion
		#region edit
		[Authorize(Roles = RL.RoleAdmin)]
		public IActionResult EditProduct(int id)
        {
            Product product = _db.Products.FirstOrDefault(x => x.Id == id);
            _db.SaveChanges();
            return View(product);

        }
        [HttpPost]
		[Authorize(Roles = RL.RoleAdmin)]
		public IActionResult EditProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
            
                return View(product);
            }
            else
            {
                Product pro = _db.Products.SingleOrDefault(x => x.Id == product.Id);
                pro.Name = product.Name;
                pro.Description = product.Description;
                pro.Price = product.Price;
                pro.Quantity = product.Quantity;
                _db.Update(pro);
                _db.SaveChanges();
                return RedirectToAction("GetAllData");
            }
           
        }
		#endregion
		public IActionResult ContactUs()
		{
            List<Contact> conts = _db.Contacts.ToList();
           
            return View(conts);
            

		}
        [HttpPost]
        public IActionResult Blogs(Blogy b)
        {
            if (b.BlogImage != null)
            {
                string ImageFolder = Path.Combine(host.WebRootPath, "images");
                string Imagepath = Path.Combine(ImageFolder, b.BlogImage.FileName);
                b.BlogImage.CopyTo(new FileStream(Imagepath, FileMode.Create));
                b.ImagesPath = b.BlogImage.FileName;
            }
           
           
            _db.blogs.Add(b);
 b.BlogDate = DateTime.Now;
            _db.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Blogs ()
        {
            return View();
        }
        public IActionResult ViewBlog()
        {
            return View(_db.blogs);
        }
        #region deleteblog
        public IActionResult DeleteBlog(int id)
        {
            Blogy blog = _db.blogs.FirstOrDefault(x => x.BlogId == id); //get id i wanna delete
            _db.blogs.Remove(blog);
            _db.SaveChanges();
            return RedirectToAction("ViewBlog");
        }
        #endregion
        #region editblog
        public IActionResult EditBlog(int id)
        {
            Blogy blog = _db.blogs.FirstOrDefault(x => x.BlogId == id);
            _db.SaveChanges();
            return View(blog);

        }
        [HttpPost]
        public IActionResult EditBlog(Blogy blogy)
        {
           

                Blogy blogg = _db.blogs.SingleOrDefault(x => x.BlogId == blogy.BlogId);
                blogg.Title = blogy.Title;
                blogg.Description = blogy.Description;
                blogg.Author = blogy.Author;

                _db.Update(blogg);
                _db.SaveChanges();
                return RedirectToAction("ViewBlog");
            

        }
        #endregion
    }
}
