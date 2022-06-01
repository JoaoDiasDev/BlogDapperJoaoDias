using BlogDapperJoaoDias.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogDapperJoaoDias.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        AdminService adminService;

        public LoginController(IServiceProvider serviceProvider)
        {
            adminService = serviceProvider.GetRequiredService<AdminService>();
        }
        public IActionResult Index()
        {
            var admin = new Entities.Admin();

            return View(admin);
        }
    }
}
