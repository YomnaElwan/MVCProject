using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineRestaurant.Models;
using OnlineRestaurant.Repository;
using OnlineRestaurant.ViewModels;
using TequliasRestaurant.Models;


namespace OnlineRestaurant.Controllers
{
    public class OrderController : Controller
    {
        Generic_Repository<Order> order_repo;
        Generic_Repository<Product> product_repo;
        private UserManager<ApplicationUser> UserManager;
        private readonly Generic_Repository<OrderItem> orderItem_Rebo;

        public OrderController(Generic_Repository<Order> orderrepo,
            Generic_Repository<Product> productrepo,
            UserManager<ApplicationUser> UserManager,
            Generic_Repository<OrderItem> OrderItem_rebo
            )


        {
            this.order_repo = orderrepo;
            this.product_repo = productrepo;
            this.UserManager = UserManager;
            orderItem_Rebo = OrderItem_rebo;
        }
        [HttpPost]

        public IActionResult AddOrderItem(int ProductId, int Quantity)
        {
            Product product = product_repo.GetById(ProductId);
            if (product == null)
            {
                return NotFound();
            }


            var model = HttpContext.Session.Get<OrderVM>("OrderVM") ?? new OrderVM()
            {
                products = product_repo.GetAll(),
                orderItemVMs = new List<OrderItemVM>()
            };



            OrderItemVM orderItemVM = model.orderItemVMs.FirstOrDefault(orderVM => orderVM.ProductId == ProductId);

            if (orderItemVM != null)
            {
                orderItemVM.Quantity += Quantity;
            }
            else
            {
                model.orderItemVMs.Add(
                    new OrderItemVM()
                    {
                        ProductId = ProductId,
                        Quantity = Quantity,
                        Price = product.Price,
                        ProductName = product.Name
                    });
            }
            model.TotalAmount = model.orderItemVMs.Sum(s => s.Price * s.Quantity);
            HttpContext.Session.Set("OrderVM", model);

            return RedirectToAction("CreateOrder", model);
        }
     
        [Authorize]
        [HttpGet]

        public async Task<IActionResult> CreateOrder()
        {
            var orderVM = HttpContext.Session.Get<OrderVM>("OrderVM") ?? new OrderVM()
            {
                products = product_repo.GetAll(),
                orderItemVMs = new List<OrderItemVM>()
            };

            return View(orderVM);
        }
        [HttpGet]

        public IActionResult Cart()
        {
            var model = HttpContext.Session.Get<OrderVM>("OrderVM");
            if (model != null)
            {
                return View(model);
            }
            return RedirectToAction("CreateOrder");
        }
        public IActionResult PlaceOrder()
        {
            var model = HttpContext.Session.Get<OrderVM>("OrderVM");
            if (model != null || !model.orderItemVMs.Any() ||model.orderItemVMs==null)
            {
                Order order = new Order()
                {
                    UserId = UserManager.GetUserId(User),
                    Date = DateTime.Now,
                    TotalAmount = model.TotalAmount,
                    OrderItems= new List<OrderItem>()
                };

                foreach (var item in model.orderItemVMs)
                {
                    order.OrderItems.Add(new OrderItem()
                    {
                        ProductId = item.ProductId,
                        Price = item.Price,
                        Quantity = item.Quantity,

                    });


                }

                order_repo.Add(order);
                order_repo.Save();
                return RedirectToAction("GetOrder", new {id=order.Id });
            }
            return RedirectToAction("CreateOrder");
        }



        [Authorize]
        public IActionResult GetOrder(int id)
        {
            Order order = order_repo.GetById(id);
            
           
            return (order==null||order.OrderItems==null||!order.OrderItems.Any())? NotFound(): View("GetOrder", order);
        }
        [Authorize (Roles ="Admin")]
        public IActionResult GetAllOrder()
        {
            List<Order> orders= order_repo.GetAll();
            return View(orders);
        }
    }
}
