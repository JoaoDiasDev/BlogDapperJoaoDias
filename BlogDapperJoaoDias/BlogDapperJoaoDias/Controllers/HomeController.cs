using BlogDapperJoaoDias.Models;
using BlogDapperJoaoDias.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlogDapperJoaoDias.Controllers
{
    public class HomeController : Controller
    {
        private CategoryService _categoryService;
        private ArticleService _articleService;

        public HomeController(IServiceProvider serviceProvider)
        {
            _categoryService = serviceProvider.GetRequiredService<CategoryService>();
            _articleService = serviceProvider.GetRequiredService<ArticleService>();
        }

        public IActionResult Index()
        {
            var categories = _categoryService.GetAll();
            var articles = _articleService.GetHome();
            var model = new GeneralViewModel
            {
                CategoryList = categories,
                ArticleList = articles
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}