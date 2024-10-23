namespace Project.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImagePath { get; set; }

        public int? CategoryId { get; set; }//Đây là khóa ngoại , cách đặt tên khóa ngoại là Tên của bảng chính+Id
        public Category? Category { get; set; }// Khai báo để lấy được thông tin của Category
        public ICollection<OrderProduct>? OrderProducts { get; set; }
        public string? Price { get; set; }
    }
}
