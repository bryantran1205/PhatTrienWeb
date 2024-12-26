namespace Project.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string ?ProductName { get; set; }
        public string ?ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total => Price * Quantity;
    }

    public class Cart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public decimal SubTotal { get; set; } // Tổng tiền trước thuế
        public decimal Tax { get; set; } // Thuế
        public decimal GrandTotal { get; set; } // Tổng tiền sau thuế
    }
}
