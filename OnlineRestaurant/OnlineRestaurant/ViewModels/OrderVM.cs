using OnlineRestaurant.Models;

namespace OnlineRestaurant.ViewModels
{
    public class OrderVM
    {

        public IEnumerable<Product> products { get; set; }
        public List<OrderItemVM> orderItemVMs{ get; set; }
        public decimal TotalAmount { get; set; }

    }
}
