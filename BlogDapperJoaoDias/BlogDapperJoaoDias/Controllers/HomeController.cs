using BlogDapperJoaoDias.Models;
using BlogDapperJoaoDias.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlogDapperJoaoDias.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private CategoryService categoryService;

        public HomeController(ILogger<HomeController> logger, CategoryService _categoryService)
        {
            _logger = logger;
            categoryService = _categoryService;
        }

        public IActionResult Index()
        {
            var categories = categoryService.GetAll();
            var model = new GeneralViewModel
            {
                CategoryList = categories
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