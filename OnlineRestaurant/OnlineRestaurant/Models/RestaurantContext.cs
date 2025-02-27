using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OnlineRestaurant.Models
{
    public class RestaurantContext:IdentityDbContext<ApplicationUser>
    {
        public RestaurantContext() : base()
        {

        }
        public RestaurantContext(DbContextOptions<RestaurantContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies(true);

            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductIngredient>().HasKey(PI => new {PI.productId,PI.ingredientId});
            base.OnModelCreating(modelBuilder);

        }
        public virtual DbSet<Product>Products { get; set; }
        public virtual DbSet<Category>Categories { get; set; }
        public virtual DbSet<Order>Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItem { get; set; }
        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<ProductIngredient> ProductIngredients { get; set; }



    }
}
