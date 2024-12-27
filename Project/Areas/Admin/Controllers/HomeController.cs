using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.AppData;
using Project.Models; // Sử dụng model Product
using System.Linq;

namespace Project.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")] // Chỉ cho phép Admin truy cập
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly AppDBContext _db;

        public ProductController(AppDBContext dbContext)
        {
            _db = dbContext;
        }

        // Hiển thị danh sách sản phẩm
        public IActionResult Index()
        {
            var products = _db.Products.ToList();
            return View(products);
        }

        // Thêm sản phẩm (Hiển thị form)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Thêm sản phẩm (Xử lý form)
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _db.Products.Add(product);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // Sửa sản phẩm (Hiển thị form)
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // Sửa sản phẩm (Xử lý form)
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _db.Products.Update(product);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // Xóa sản phẩm
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var product = _db.Products.Find(id);
            if (product != null)
            {
                _db.Products.Remove(product);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
