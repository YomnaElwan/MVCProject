using OnlineRestaurant.Validation;
using System.ComponentModel.DataAnnotations;

namespace OnlineRestaurant.ViewModels
{
    public class UserRegisterVM
    {
        [Required]
        public string UserName { get; set; }
        //[Unique]

        [Required]

        public string Email { get; set; }
        public  string Address { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password" ,ErrorMessage ="Passwords Should match")]
        [DataType(DataType.Password)]

        public string ConfirmPassword { get; set; }


    }
}
