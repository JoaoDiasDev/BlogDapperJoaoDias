using BlogDapperJoaoDias.Areas.Admin.Models;
using BlogDapperJoaoDias.Models;
using BlogDapperJoaoDias.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogDapperJoaoDias.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuth]
    public class CategoryController : Controller
    {
        private CategoryService _categoryService;

        public CategoryController(IServiceProvider serviceProvider)
        {
            _categoryService = serviceProvider.GetRequiredService<CategoryService>();
        }

        [Route("/Admin/Category")]
        public IActionResult Index()
        {
            var categoryList = _categoryService.GetAll();

            var model = new UserViewModel
            {
                CategoryList = categoryList
            };
            return View(model);
        }

        [HttpGet]
        [Route("/Admin/Category/Add")]
        public IActionResult Add()
        {
            Entities.Category category = new Entities.Category();
            return View(category);
        }

        [Route("/Admin/Category/Add")]
        [HttpPost, AutoValidateAntiforgeryToken]
        public IActionResult Add(Entities.Category category)
        {
            int result = _categoryService.Add(category);
            if (result == 0)
            {
                ViewBag.Error = "Something went wrong please try it again!";
                return View(category);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }


        [Route("/Admin/Category/Edit")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Entities.Category category = _categoryService.Get(id);
            return View(category);
        }

        [Route("/Admin/Category/Edit")]
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(Entities.Category category)
        {
            var result = _categoryService.Update(category);
            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Error = "Something went wrong please try it again!";
                return View(category);
            }
        }

        [Route("/Admin/Category/Delete")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var category = _categoryService.Get(id);
            return View(category);
        }

        [Route("/Admin/Category/Delete")]
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormFile file)
        {
            var category = _categoryService.Get(id);
            bool result = _categoryService.Delete(category);
            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Error = "Something went wrong please try it again!";
                return View(category);
            }
        }
    }
}
