using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace OnlineRestaurant.Models
{
    public class ApplicationUser:IdentityUser
    {
        
       
        [Required]
        public string Password { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public virtual List<Order> Orders { get; set; }

    }
}
