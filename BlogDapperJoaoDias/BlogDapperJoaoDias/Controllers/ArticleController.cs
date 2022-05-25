using Microsoft.AspNetCore.Mvc;

namespace BlogDapperJoaoDias.Controllers
{
    public class ArticleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }
    }
}
