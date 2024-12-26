using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Project.AppData;
using Project.Models;

namespace Project.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDBContext _db;
        private static List<CartItem> _cart = new List<CartItem>();

        public ProductController(AppDBContext db)
        {
            _db = db;
        }
        public IActionResult Cart()
        {
            var cart = new Cart
            {
                Items = _cart
            };
            return View(cart);
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
        [HttpPost]
        public ActionResult AddToCart(int productId, string productName, string imageUrl, decimal price)
        {
            var item = _cart.FirstOrDefault(x => x.ProductId == productId);
            if (item != null)
            {
                item.Quantity++; // Tăng số lượng nếu sản phẩm đã có
            }
            else
            {
                _cart.Add(new CartItem
                {
                    ProductId = productId,
                    ProductName = productName,
                    ImageUrl = imageUrl,
                    Price = price,
                    Quantity = 1
                });
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult UpdateQuantity(int productId, int quantity)
        {
            var item = _cart.FirstOrDefault(x => x.ProductId == productId);
            if (item != null)
            {
                item.Quantity = quantity;
                if (item.Quantity <= 0)
                    _cart.Remove(item); // Xóa nếu số lượng <= 0
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RemoveFromCart(int productId)
        {
            var item = _cart.FirstOrDefault(x => x.ProductId == productId);
            if (item != null)
            {
                _cart.Remove(item);
            }

            return RedirectToAction("Index");
        }
    }
}
