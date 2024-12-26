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
        private const string CartSessionKey = "Cart";
        public ProductController(AppDBContext db)
        {
            _db = db;
        }
        // Lấy giỏ hàng từ Session
        private List<CartItem> GetCartFromSession()
        {
            var cartJson = HttpContext.Session.GetString(CartSessionKey);
            if (!string.IsNullOrEmpty(cartJson))
            {
                return JsonConvert.DeserializeObject<List<CartItem>>(cartJson);
            }
            return new List<CartItem>();
        }
        private void SaveCartToSession(List<CartItem> cart)
        {
            var cartJson = JsonConvert.SerializeObject(cart);
            HttpContext.Session.SetString(CartSessionKey, cartJson);
        }
        public IActionResult Cart()
        {
            var cart = new Cart
            {
                Items = GetCartFromSession()
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

        // Thêm sản phẩm vào giỏ hàng
        [HttpPost]
        public IActionResult AddToCart(int productId, string productName, string imageUrl, decimal price)
        {
            var cart = GetCartFromSession();

            // Kiểm tra sản phẩm đã tồn tại trong giỏ hàng hay chưa
            var item = cart.FirstOrDefault(x => x.ProductId == productId);
            if (item != null)
            {
                item.Quantity++; // Tăng số lượng nếu đã tồn tại
            }
            else
            {
                // Thêm sản phẩm mới vào giỏ hàng
                cart.Add(new CartItem
                {
                    ProductId = productId,
                    ProductName = productName,
                    ImageUrl = imageUrl,
                    Price = price,
                    Quantity = 1
                });
            }

            SaveCartToSession(cart); // Cập nhật giỏ hàng vào Session
            return Json(new { success = true, message = "Product added to cart successfully!" });
        }

        [HttpPost]
        public IActionResult UpdateQuantity(int productId, int quantity)
        {
            var cart = GetCartFromSession();

            var item = cart.FirstOrDefault(x => x.ProductId == productId);
            if (item != null)
            {
                item.Quantity = quantity;

                // Xóa sản phẩm nếu số lượng <= 0
                if (item.Quantity <= 0)
                {
                    cart.Remove(item);
                }
            }

            SaveCartToSession(cart); // Cập nhật lại giỏ hàng trong Session

            // Tính toán lại các giá trị
            var newTotal = item?.Quantity * item?.Price ?? 0;
            var subTotal = cart.Sum(x => x.Quantity * x.Price);
            var grandTotal = subTotal + (subTotal * 0.1m);

            return Json(new
            {
                success = true,
                newTotal = newTotal,
                subTotal = subTotal,
                grandTotal = grandTotal
            });
        }


        [HttpPost]
        public IActionResult RemoveFromCart(int productId)
        {
            var cart = GetCartFromSession();

            // Tìm và xóa sản phẩm khỏi giỏ hàng
            var item = cart.FirstOrDefault(x => x.ProductId == productId);
            if (item != null)
            {
                cart.Remove(item);
            }

            SaveCartToSession(cart); // Cập nhật lại giỏ hàng trong Session

            // Tính toán lại các giá trị
            var subTotal = cart.Sum(x => x.Quantity * x.Price);
            var tax = subTotal * 0.1m; // Thuế 10%
            var grandTotal = subTotal + tax;

            return Json(new
            {
                success = true,
                subTotal = subTotal,
                tax = tax,
                grandTotal = grandTotal
            });
        }
    }
}
