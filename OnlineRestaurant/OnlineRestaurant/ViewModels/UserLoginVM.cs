using System.ComponentModel.DataAnnotations;

namespace OnlineRestaurant.ViewModels
{
    public class UserLoginVM
    {
        public bool RememberMe {  get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]

        public string Password { get; set; }
    }
}
