using BlogDapperJoaoDias.Areas.Admin.Models;
using BlogDapperJoaoDias.Models;
using BlogDapperJoaoDias.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogDapperJoaoDias.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuth]
    public class ArticleController : Controller
    {
        private ArticleService _articleService;
        private CategoryService _categoryService;
        private CityService _cityService;

        public ArticleController(IServiceProvider serviceProvider)
        {
            _articleService = serviceProvider.GetRequiredService<ArticleService>();
            _categoryService = serviceProvider.GetRequiredService<CategoryService>();
            _cityService = serviceProvider.GetRequiredService<CityService>();
        }
        [Route("/Admin/Article")]
        public IActionResult Index()
        {
            var articleList = _articleService.GetAll();
            var model = new UserViewModel
            {
                ArticleList = articleList
            };
            return View(model);
        }

        [Route("/Admin/Article/Delete")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var article = _articleService.GetById(id);
            return View(article);
        }

        [Route("/Admin/Article/Delete")]
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormFile file)
        {
            var article = _articleService.GetById(id);
            bool result = _articleService.Delete(article);
            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Error = "Something went wrong please try it again!";
                return View(article);
            }
        }

        public IActionResult Edit(int id)
        {
            var article = _articleService.GetById(id);
            var categories = _categoryService.GetAll();
            var cities = _cityService.GetAll();

            var model = new UserViewModel
            {
                Article = article,
                CategoryList = categories,
                CityList = cities
            };

            return View(model);
        }
    }
}
