using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Order
    {
        public int? Id { get; set; }
        [StringLength(450)]// chiều dài bằng userid trong bảng user dùng cho identity
        public string UserId { get; set; } = null!;//Đây là khóa ngoại của User
        public string? Status { get; set; }
        public DateTime? CreatedAt { get; set; }

        public ICollection<OrderProduct>? OrderProducts { get; set; }
    }
}
