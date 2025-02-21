using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineRestaurant.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }

        public string? UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual List<OrderItem> OrderItems{ get; set; }


    }
}
