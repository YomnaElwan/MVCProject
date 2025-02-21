using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using OnlineRestaurant.Models;
using OnlineRestaurant.Repository;
using OnlineRestaurant.ViewModels;

namespace OnlineRestaurant.Controllers
{
    public class CategoryController : Controller
    {
        Generic_Repository<Category> category_Repo;
        public CategoryController(Generic_Repository<Category> repo)
        {
            this.category_Repo = repo;   
        }

        public IActionResult Getall()
        {
            return View("AllCategories",category_Repo.GetAll());
        }

        public IActionResult New()
        {
            return View("New");
        }


        [HttpPost]
        public IActionResult SaveNew(Category category)
        {
            if (ModelState.IsValid == true)
            {
                category_Repo.Add(category);
                category_Repo.Save();
            }
            return View("New", category);

        }

        public IActionResult Edit(int id)
        {
            return View("Edit", category_Repo.GetById(id));
        }

        [HttpPost]
        public IActionResult SaveEdit(Category category)
        {
            if (ModelState.IsValid)
            {
                category_Repo.Update(category);
                category_Repo.Save();
            }
            return View("Edit", category);
        }

        [HttpGet]

        public IActionResult Delete(int id) {

            category_Repo.Delete(id);
            if (category_Repo.Save() > 0)
            {
                category_Repo.Save();

            }

            return View("AllCategories",category_Repo.GetAll());
        }

        public IActionResult Details(int id)
        {
            
            return View("Details", category_Repo.GetById(id)); 
        }


    }
}
