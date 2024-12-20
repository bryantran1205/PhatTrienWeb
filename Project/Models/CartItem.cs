namespace Project.Models
{
    public class CartItem
    {
        public int quantity { get; set; }
        public Product ?product { set; get; }

    }
}
