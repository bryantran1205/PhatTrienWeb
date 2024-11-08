using Microsoft.AspNetCore.Mvc;
using Project.AppData;
using Project.Models;

namespace Project.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDBContext _db;
        public ProductController(AppDBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Detail(int id)
        {
            Product p = _db.Products.Find(id);
            List<Product> Lispro = _db.Products.ToList();
            ViewBag.Lispro = Lispro;
            if (p == null)
            {
                return NotFound();
            }
            return View(p);
        }
        [Route("product/listpro/{id}")]
        public IActionResult ListPro(int id)
        {
            List<Product> proList;

            if (id == 0)
            {
                proList = _db.Products.ToList();
            }
            else
            {
                proList = _db.Products.Where(p => p.CategoryId == id).ToList();
            }
            return View(proList);
        }

    }
}
