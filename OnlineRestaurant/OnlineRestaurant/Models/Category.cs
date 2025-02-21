namespace OnlineRestaurant.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }


        public virtual List<Product>?product { get; set; }
    }
}
