using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineRestaurant.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        [ForeignKey("order")]
        public int OrderId { get; set; }
        public virtual Order order { get; set; }

        [ForeignKey("product")]
        public int ProductId { get; set; }
        public virtual Product product { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }


    }
}
