using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using OnlineRestaurant.Models;
using System.ComponentModel.DataAnnotations;

namespace OnlineRestaurant.Validation
{
    public class UniqueAttribute:ValidationAttribute
    {
        RestaurantContext context = new RestaurantContext();
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            //if (IdentityDbContext<ApplicationUser>FirstOrDefault(c => c.Email == value.ToString()) == null)
            //{
            //    return ValidationResult.Success;

            //}

            return new ValidationResult($"{value.ToString()} Already Exists");
        }
    }
}
