using BlogDapperJoaoDias.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogDapperJoaoDias.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuth]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
