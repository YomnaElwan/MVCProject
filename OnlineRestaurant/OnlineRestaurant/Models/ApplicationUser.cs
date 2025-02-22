using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace OnlineRestaurant.Models
{
    public class ApplicationUser : IdentityUser
    {

        public string? Address { get; set; }
        public virtual List<Order> Orders { get; set; }

    }
}
