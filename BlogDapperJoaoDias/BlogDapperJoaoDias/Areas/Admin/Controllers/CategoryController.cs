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
    }
}
