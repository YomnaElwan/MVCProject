using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineRestaurant.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        [ForeignKey("category")]
        public int CategoryId { get; set; }
        public virtual Category category { get; set; }
        public virtual List<OrderItem> orderitem { get; set; }

        public virtual List <ProductIngredient> productingredient { get; set; }

    }
}
