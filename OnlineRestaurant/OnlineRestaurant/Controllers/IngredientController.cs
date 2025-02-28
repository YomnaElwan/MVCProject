using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineRestaurant.Models;
using OnlineRestaurant.Repository;

namespace OnlineRestaurant.Controllers
{

    public class IngredientController : Controller
    {
        Generic_Repository<Ingredient> ingredient_Repo;
        public IngredientController(Generic_Repository<Ingredient> ingredientRepo)

        {
            this.ingredient_Repo = ingredientRepo;
        }
        //Get All Ingredient 
        public IActionResult GetAll()
        {
            return View("Index", ingredient_Repo.GetAll());
        }

        //Add New Ingredient
        public IActionResult New()
        {
            return View("New");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveNew(Ingredient ingredient)
        {
            if (ModelState.IsValid == true)
            {
                ingredient_Repo.Add(ingredient);
                ingredient_Repo.Save();
                return RedirectToAction("GetAll");
            }
            return View("New", ingredient);

        }
        //Edit Ingredient

        public IActionResult Edit(int id)
        {
            return View("Edit", ingredient_Repo.GetById(id));
        }

        [HttpPost]
        public IActionResult SaveEdit(Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                ingredient_Repo.Update(ingredient);
                ingredient_Repo.Save();
                return RedirectToAction("GetAll");

            }
            return View("Edit", ingredient);
        }


        //Delete Ingredient

        [HttpGet]

        public IActionResult Delete(int id)
        {
            var ingredient = ingredient_Repo.GetById(id);
            if (ingredient == null)
            {
                return NotFound();
            }

            ingredient_Repo.Delete(id);
            if (ingredient_Repo.Save() > 0)
            {
                ingredient_Repo.Save();
            }

            return RedirectToAction("GetAll");
        }


        public IActionResult Details(int id)
        {

            return View("Details", ingredient_Repo.GetById(id));
        }
    }
}
