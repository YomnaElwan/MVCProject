using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineRestaurant.Models
{
    public class ProductIngredient
    {
        [ForeignKey("product")]
        public int productId { get; set; }
        public virtual Product product { get; set; }


        [ForeignKey("ingredient")]
        public int ingredientId { get; set; }
        public virtual Ingredient ingredient { get; set; }

    }
}
