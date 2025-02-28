using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineRestaurant.Models;
using OnlineRestaurant.Repository;
using OnlineRestaurant.ViewModels;
using TequliasRestaurant.Models;
using Newtonsoft.Json;
using Microsoft.CodeAnalysis;

namespace OnlineRestaurant.Controllers
{
    public class OrderController : Controller
    {
        private readonly Generic_Repository<Order> order_repo;
        private readonly Generic_Repository<Product> product_repo;
        private readonly UserManager<ApplicationUser> UserManager;
        private readonly Generic_Repository<OrderItem> orderItem_Rebo;

        public OrderController(Generic_Repository<Order> orderrepo,
            Generic_Repository<Product> productrepo,
            UserManager<ApplicationUser> userManager,
            Generic_Repository<OrderItem> orderItem_rebo)
        {
            this.order_repo = orderrepo;
            this.product_repo = productrepo;
            this.UserManager = userManager;
            this.orderItem_Rebo = orderItem_rebo;
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
                model.orderItemVMs.Add(new OrderItemVM()
                {
                    ProductId = ProductId,
                    Quantity = Quantity,
                    Price = product.Price,
                    ProductName = product.Name
                });
            }

            model.TotalAmount = model.orderItemVMs.Sum(s => s.Price * s.Quantity);
            HttpContext.Session.Set("OrderVM", model);

            return RedirectToAction("CreateOrder"); // FIX: Removed passing model as a parameter
        }

        [HttpGet]
        public IActionResult CreateOrder()
        {
            var orderVM = HttpContext.Session.Get<OrderVM>("OrderVM") ?? new OrderVM()
            {
                products = product_repo.GetAll(),
                orderItemVMs = new List<OrderItemVM>()
            };

            return View(orderVM);
        }
        public IActionResult Edit(int id)
        {
            return View("Edit", order_repo.GetById(id));
        }

        [HttpPost]
        public IActionResult SaveEdit(Order order)
        {
            if (ModelState.IsValid)
            {
                order_repo.Update(order);
             
                order_repo.Save();
            }
            return View("Edit", order);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = order_repo.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            order_repo.Delete(id);
            if (order_repo.Save() > 0)
            {
                order_repo.Save();
            }

            return RedirectToAction("GetAllOrder");
        }


        public IActionResult Cart()
        {
            var model = HttpContext.Session.Get<OrderVM>("OrderVM");
            return model != null ? View(model) : RedirectToAction("CreateOrder");
        }

        [HttpPost]
        public IActionResult PlaceOrder()
        {
            var model = HttpContext.Session.Get<OrderVM>("OrderVM");

            if (model == null || model.orderItemVMs == null || !model.orderItemVMs.Any())
            {
                return RedirectToAction("CreateOrder");
            }

            Order order = new Order()
            {
                UserId = UserManager.GetUserId(User),
                Date = DateTime.Now,
                TotalAmount = model.TotalAmount,
                OrderItems = model.orderItemVMs.Select(item => new OrderItem()
                {
                    ProductId = item.ProductId,
                    Price = item.Price,
                    Quantity = item.Quantity
                }).ToList()
            };

            order_repo.Add(order);
            order_repo.Save();

            HttpContext.Session.Remove("OrderVM"); // Clear the session after placing order
           
            return RedirectToAction("GetOrder", new { id = order.Id });
        }

        public IActionResult GetOrder(int id)
        {
            Order order = order_repo.GetById(id);
            return (order == null || order.OrderItems == null || !order.OrderItems.Any()) ? NotFound() : View("GetOrder", order);
        }

        //[Authorize(Roles = "Admin")]
        public IActionResult GetAllOrder()
        {
            List<Order> orders = order_repo.GetAll();
            return View(orders);
        }

        public IActionResult DeleteOrderItem(int id)
        {
            var model = HttpContext.Session.Get<OrderVM>("OrderVM");

            if (model == null || model.orderItemVMs == null || !model.orderItemVMs.Any())
            {
                return RedirectToAction("CreateOrder"); 
            }

            var orderItemVM = model.orderItemVMs.FirstOrDefault(orderVM => orderVM.ProductId == id);

            if (orderItemVM != null)
            {
                model.orderItemVMs.Remove(orderItemVM); 
                model.TotalAmount = model.orderItemVMs.Sum(s => s.Price * s.Quantity); 
                HttpContext.Session.Set("OrderVM", model);
            }

            return RedirectToAction("Cart");
        }

    }
}