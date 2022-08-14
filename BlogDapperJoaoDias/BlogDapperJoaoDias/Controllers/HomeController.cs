using BlogDapperJoaoDias.Helpers;
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
        private IServiceProvider _service;

        public HomeController(IServiceProvider serviceProvider)
        {
            _categoryService = serviceProvider.GetRequiredService<CategoryService>();
            _articleService = serviceProvider.GetRequiredService<ArticleService>();
            _service = serviceProvider;
        }

        public IActionResult Index()
        {
            int page = HttpContext.Request.Query["page"].Count == 0 ? 1 : int.Parse(HttpContext.Request.Query["page"]);
            var paginationHelpers = new PaginationHelpers(_service);
            var paginationModel = paginationHelpers.ArticlePagination(page);
            var model = new GeneralViewModel
            {
                PaginationModel = paginationModel
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

        [Route("/SearchResults")]
        public IActionResult Search()
        {
            var searchQuery = HttpContext.Request.Query["q"];
            var articles = _articleService.Search(searchQuery);
            var model = new GeneralViewModel
            {
                ArticleList = articles
            };

            return View(model);
        }
    }
}