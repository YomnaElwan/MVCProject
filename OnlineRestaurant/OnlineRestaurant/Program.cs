using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineRestaurant.Models;
using OnlineRestaurant.Repository;
using OnlineRestaurant.UnitOfWork;

namespace OnlineRestaurant
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<RestaurantContext>(options =>
            {
                options.UseLazyLoadingProxies(true).UseSqlServer(builder.Configuration.GetConnectionString("RestaurantDatabase"));


            });
            //builder.Services.AddScoped<RestaurantUnitWork>();

            builder.Services.AddScoped(typeof(Generic_Repository<>));

            builder.Services.AddScoped<IRepository<Category>, Generic_Repository<Category>>();
            builder.Services.AddScoped<IRepository<Product>, Generic_Repository<Product>>();
            builder.Services.AddScoped<IRepository<Ingredient>, Generic_Repository<Ingredient>>();
            builder.Services.AddScoped<IRepository<Order>, Generic_Repository<Order>>();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<RestaurantContext>();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Set your timeout here
            });

            var app = builder.Build();
            app.UseSession();
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
               pattern: "{controller=Home}/{action=Index}/{id?}")
               //pattern: "{controller=Category}/{action=Getall}/{id?}")

                .WithStaticAssets();

            app.Run();
        }
    }
}
