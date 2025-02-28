using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineRestaurant.Models;
using OnlineRestaurant.Repository;
using OnlineRestaurant.ViewModels;

namespace OnlineRestaurant.Controllers
{
    public class AccountController : Controller
    {
        UserManager<ApplicationUser> userManager;
        SignInManager<ApplicationUser> signInManager;
        Generic_Repository<Order> orderRepo;
        Generic_Repository<Category> categories;
        Generic_Repository<Product> productRepo;
        public AccountController
            (UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, Generic_Repository<Order> orderRepo, Generic_Repository<Category> categories, Generic_Repository<Product> productRepo
)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.orderRepo = orderRepo;
            this.categories = categories;
            this.productRepo = productRepo;
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
        public IActionResult AdminDashBoard()
        {
            AdminMainDashboardVM adminDB = new AdminMainDashboardVM();
            adminDB.users = userManager.Users.Count();
            adminDB.orders = orderRepo.GetAll().Count();
            adminDB.categories = categories.GetAll().Count();
            adminDB.products = productRepo.GetAll().Count();
            return View("AdminDashBoard",adminDB);
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
                        var Role = await userManager.GetRolesAsync(user);
                        if (Role.Contains("Admin"))
                        {
                           return RedirectToAction("AdminDashBoard","Account");
                        }
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
