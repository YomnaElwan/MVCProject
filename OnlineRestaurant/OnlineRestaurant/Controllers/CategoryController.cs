using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using OnlineRestaurant.Models;
using OnlineRestaurant.Repository;
using OnlineRestaurant.UnitOfWork;
using OnlineRestaurant.ViewModels;

namespace OnlineRestaurant.Controllers
{
    
    public class CategoryController : Controller
    {
        //RestaurantUnitWork RestaurantUnitOfWork;
        Generic_Repository<Category> category_Repo;
        public CategoryController(
                Generic_Repository<Category> repo
                //RestaurantUnitWork RestaurantUnitWork
            )
        {
            this.category_Repo = repo;
            //this.RestaurantUnitOfWork = RestaurantUnitWork;
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

        public IActionResult Delete(int id)
        {
            var category = category_Repo.GetById(id);
            if (category == null)
            {
                return NotFound();
            }

            var productRepo = new Generic_Repository<Product>(category_Repo.ctxt);
            var products = productRepo.ctxt.Products.Where(p => p.CategoryId == id).ToList();

            foreach (var product in products)
            {
                productRepo.Delete(product.Id);
            }
            productRepo.Save(); 

            category_Repo.Delete(id);
            category_Repo.Save();

            return RedirectToAction("Getall"); 
        }

        public IActionResult Details(int id)
        {
            return View("Details", category_Repo.GetById(id)); 
        }


    }
}
