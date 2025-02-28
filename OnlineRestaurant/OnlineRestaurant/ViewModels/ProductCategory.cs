using OnlineRestaurant.Models;
using System.ComponentModel.DataAnnotations;

namespace OnlineRestaurant.ViewModels
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public string imageSrc { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }
        [Display(Name = "Category")]

        public int CategoryId { get; set; }

        public List<Category>? Categories { get; set; }
    }
}
