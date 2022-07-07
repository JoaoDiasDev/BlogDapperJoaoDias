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

        public ArticleController(IServiceProvider serviceProvider)
        {
            _articleService = serviceProvider.GetRequiredService<ArticleService>();
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
    }
}
