using Microsoft.AspNetCore.Mvc;
using Project.AppData;
using Project.Models;
using System.Collections.Generic;
using System.Diagnostics;
namespace Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDBContext _db;


        public HomeController(ILogger<HomeController> logger,AppDBContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            List<Product> products = _db.Products.Take(4).ToList();
            return View(products);
        }

        public IActionResult Privacy()
        {
            List<Category> categories = _db.Categories.Take(2).ToList();
            return View(categories);
        }
        public IActionResult Product()
        {
            List<Product> products = _db.Products.ToList();
            return View(products);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
