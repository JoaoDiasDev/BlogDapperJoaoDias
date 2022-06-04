using BlogDapperJoaoDias.Areas.Admin.Models;
using BlogDapperJoaoDias.Models;
using BlogDapperJoaoDias.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogDapperJoaoDias.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuth]
    public class UserController : Controller
    {
        private AdminService _adminService;

        public UserController(IServiceProvider serviceProvider)
        {
            _adminService = serviceProvider.GetRequiredService<AdminService>();
        }
        [Route("/Admin/User")]
        public IActionResult Index()
        {
            var userList = _adminService.GetAll();

            var model = new UserViewModel
            {
                UserList = userList
            };
            return View(model);
        }

        [HttpGet]
        [Route("/Admin/User/Add")]
        public IActionResult Add()
        {
            return View();
        }

        [Route("/Admin/User/Add")]
        [HttpPost, AutoValidateAntiforgeryToken]
        public IActionResult Add(Entities.Admin admin)
        {
            return View();
        }
    }
}
