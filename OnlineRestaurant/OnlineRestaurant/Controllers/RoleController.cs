using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineRestaurant.ViewModels;

namespace OnlineRestaurant.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        public RoleController(RoleManager<IdentityRole> RoleManager)
        {
            this.roleManager = RoleManager;
        }

        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid==true)
            {
                IdentityResult identityResult=
                await roleManager.CreateAsync(new IdentityRole
                {
                    Name=roleViewModel.RoleName,
                });
                if (identityResult.Succeeded) {
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    foreach (var item in identityResult.Errors)
                    {
                        ModelState.AddModelError("Error", item.Description);
                    }
                }


            }
            return View(roleViewModel);
        }
    }
}
