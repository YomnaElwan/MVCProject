using Microsoft.AspNetCore.Mvc;
using OnlineRestaurant.Models;
using OnlineRestaurant.Repository;

namespace OnlineRestaurant.Controllers
{
    public class OrderController : Controller
    {
        Generic_Repository<Order> order_repo;
        public OrderController(Generic_Repository<Order> orderrepo)

        {
            this.order_repo = orderrepo;
            
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
