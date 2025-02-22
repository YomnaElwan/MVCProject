using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineRestaurant.Models;
using OnlineRestaurant.ViewModels;

namespace OnlineRestaurant.Controllers
{
    public class AccountController : Controller
    {
        UserManager<ApplicationUser> userManager;
        SignInManager<ApplicationUser> signInManager;
        public AccountController
            (UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }


        public IActionResult Register()
        {
            return View("Register");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterVM userRegisterVM)
        {
            if (ModelState.IsValid)
            {

                ApplicationUser appUser = new ApplicationUser()
                {
                    UserName = userRegisterVM.UserName,
                    PasswordHash = userRegisterVM.Password,
                    Address = userRegisterVM.Address,
                };
                IdentityResult result =
                   await userManager.CreateAsync(appUser, userRegisterVM.Password);
                if (result.Succeeded)
                {
                    //add to Admin Role
                    await userManager.AddToRoleAsync(appUser, "User");
                    //Create Cooike
                    await signInManager.SignInAsync(appUser, false);
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    //reason ==>enduser as modelstate error
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);

                    }
                }



            }

            return View("Register", userRegisterVM);
        }

        public IActionResult Login()
        {
            return View("Login");
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginVM userLoginVM)
        {
            if (ModelState.IsValid == true)
            {
                ApplicationUser user = await userManager.FindByNameAsync(userLoginVM.UserName);
                if (user != null)
                {
                    bool isFound = await userManager.CheckPasswordAsync(user, userLoginVM.Password);
                    if (isFound == true)
                    {
                        await signInManager.SignInAsync(user, userLoginVM.RememberMe);
                        return RedirectToAction("Index", "Home");
                    }

                }
                ModelState.AddModelError("", "Invalid Username Or Password");

            }
            return View("Login", userLoginVM);
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
