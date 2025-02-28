using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineRestaurant.Models;
using OnlineRestaurant.Repository;
using OnlineRestaurant.ViewModels;

namespace OnlineRestaurant.Controllers
{
    public class ProductController : Controller
    {
        Generic_Repository<Product> product_Repo;
        Generic_Repository<Category> category_Repo;


        public ProductController(Generic_Repository<Product> productRepo,Generic_Repository<Category>categoryRepo)
        {
            this.product_Repo = productRepo;
            this.category_Repo = categoryRepo;
        }
        //Get All Product
        [HttpGet]
        public IActionResult GetAll()
        {
            return View("AllProducts", product_Repo.GetAll());

        }

        //Add New Product
        

        public IActionResult New()
        {
            var viewModel = new ProductCategory
            {
                Categories = category_Repo.GetAll(),
            };
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult SaveNew(ProductCategory viewModel)
        {
            var newProduct = new Product
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                Price = viewModel.Price,
                Stock = viewModel.Stock,
                CategoryId = viewModel.CategoryId,
                imageSrc=viewModel.imageSrc
                
            };
            if (ModelState.IsValid == true)
            {
                try
                {
                    product_Repo.Add(newProduct);
                    product_Repo.Save();
                    return RedirectToAction("GetAll");
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError("CategoryId", "Please Select Category");


                }

            }

            viewModel.Categories = category_Repo.GetAll();
            return View("New", viewModel);
        }
        //Edit Product

        public IActionResult Edit(int id)
        {
            var product = product_Repo.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            var viewModel = new ProductCategory
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                CategoryId = product.CategoryId,
                Categories = category_Repo.GetAll() ,
                imageSrc=product.imageSrc,
            };

            return View("Edit", viewModel);
        }

        [HttpPost]
        public IActionResult SaveEdit(ProductCategory viewModel)
        {
            if (ModelState.IsValid)
            {
                var existingProduct = product_Repo.GetById(viewModel.Id);
                if (existingProduct == null)
                {
                    return NotFound();
                }

                existingProduct.Name = viewModel.Name;
                existingProduct.Description = viewModel.Description;
                existingProduct.Price = viewModel.Price;
                existingProduct.Stock = viewModel.Stock;
                existingProduct.imageSrc = viewModel.imageSrc;
                existingProduct.CategoryId = viewModel.CategoryId;

                product_Repo.Update(existingProduct);
                product_Repo.Save();

                return RedirectToAction("GetAll");
            }

            viewModel.Categories = category_Repo.GetAll();
            return View("Edit", viewModel);
        }


        //Delete Product

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = product_Repo.GetById(id);
            if (product == null)
            {
                return NotFound(); 
            }

            product_Repo.Delete(id);
            if (product_Repo.Save() > 0)
            {
                product_Repo.Save();
            }

            return RedirectToAction("GetAll");
        }


        public IActionResult Details(int id)
        {
            var product = product_Repo.ctxt.Products
                             .Where(p => p.Id == id)
                             .Include(p => p.category)
                             .FirstOrDefault();
            return View("Details",product);
        }



    }
}
