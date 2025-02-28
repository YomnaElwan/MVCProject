using Microsoft.AspNetCore.Identity;
using OnlineRestaurant.Models;
using OnlineRestaurant.Repository;

namespace OnlineRestaurant.UnitOfWork
{
    public class RestaurantUnitWork
    {
        UserManager<ApplicationUser> userManager;
        SignInManager<ApplicationUser> signInManager;
        Generic_Repository<Order> order_repo;
        Generic_Repository<Product> product_repo;
        UserManager<ApplicationUser> UserManager;
        Generic_Repository<OrderItem> orderItem_Rebo;
        Generic_Repository<Category> category_Repo;
        Generic_Repository<Ingredient> ingredient_Repo;
        RoleManager<IdentityRole> roleManager;
        RestaurantContext context;
        public RestaurantUnitWork(RestaurantContext restaurantContext,UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, Generic_Repository<Order> order_repo, Generic_Repository<Product> product_repo, Generic_Repository<OrderItem> orderItem_Rebo, Generic_Repository<Category> category_Repo, Generic_Repository<Ingredient> ingredient_Repo, RoleManager<IdentityRole> roleManager)
        {
            this.context = restaurantContext;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.order_repo = order_repo;
            this.product_repo = product_repo;
            this.orderItem_Rebo = orderItem_Rebo;
            this.category_Repo = category_Repo;
            this.ingredient_Repo = ingredient_Repo;
            this.roleManager = roleManager;
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
