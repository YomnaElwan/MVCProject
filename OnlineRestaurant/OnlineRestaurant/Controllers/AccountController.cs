using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineRestaurant.Models;
using OnlineRestaurant.ViewModels;

namespace OnlineRestaurant.Controllers
{
    public class AccountController : Controller
    {
        //UserManager<ApplicationUser> userManager;
        //SignInManager<ApplicationUser> signInManager;
        //public AccountController
        //    (UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        //{
        //    this.userManager = userManager;
        //    this.signInManager = signInManager;
        //}


        public IActionResult Register()
        {
            return View("Register");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Resiter(UserRegisterVM userRegisterVM)
        {
            //if (ModelState.IsValid) {

            //    ApplicationUser appUser = new ApplicationUser()
            //    {
            //        UserName = userRegisterVM.UserName,
            //        PasswordHash = userRegisterVM.Password,
            //        Address = userRegisterVM.Address,
            //    };
            //    IdentityResult result =
            //        await userManager.CreateAsync(appUser, userRegisterVM.Password);
            //    if (result.Succeeded)
            //    {
            //        //add to Admin Role
            //        await userManager.AddToRoleAsync(appUser, "Admin");
            //        //Create Cooike
            //        await signInManager.SignInAsync(appUser, false);
            //        return RedirectToAction("Index", "Department");
            //    }
            //    else
            //    {
            //        //reason ==>enduser as modelstate error
            //        foreach (var item in result.Errors)
            //        {
            //            ModelState.AddModelError("", item.Description);

            //        }
            //    }



            //}

            return View("Register",userRegisterVM);
        }

        public IActionResult Login()
        {
            return View("Login");
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Login(UserLoginVM userLoginVM)
        {
            return View("Login", userLoginVM);
        }

    }
}
