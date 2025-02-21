using OnlineRestaurant.Validation;
using System.ComponentModel.DataAnnotations;

namespace OnlineRestaurant.ViewModels
{
    public class UserRegisterVM
    {
        [Required]
        [Unique]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        public  string Address { get; set; }
        [Required]
        [MinLength(6,ErrorMessage ="Password must be at least 6 chars")]
        public string Password { get; set; }
        [Compare("Password" ,ErrorMessage ="Passwords Should match")]
        public string ConfirmPassword { get; set; }


    }
}
