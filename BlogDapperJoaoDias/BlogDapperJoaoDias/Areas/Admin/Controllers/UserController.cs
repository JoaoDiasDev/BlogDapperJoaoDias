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
            Entities.Admin admin = new Entities.Admin();
            return View(admin);
        }

        [Route("/Admin/User/Add")]
        [HttpPost, AutoValidateAntiforgeryToken]
        public IActionResult Add(Entities.Admin admin)
        {
            int result = _adminService.Add(admin);
            if (result == 0)
            {
                ViewBag.Error = "Something went wrong please try it again!";
                return View(admin);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [Route("/Admin/User/Edit")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Entities.Admin admin = _adminService.Get(id);
            return View(admin);
        }

        [Route("/Admin/User/Edit")]
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Entities.Admin admin)
        {
            Entities.Admin result = _adminService.Update(admin);
            if (result == null)
            {
                ViewBag.Error = "Something went wrong please try it again!";
                return View(admin);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [Route("/Admin/User/Delete")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Entities.Admin admin = _adminService.Get(id);
            return View(admin);
        }

        [Route("/Admin/User/Delete")]
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormFile file)
        {
            Entities.Admin admin = _adminService.Get(id);
            bool result = _adminService.Delete(admin);
            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Error = "Something went wrong please try it again!";
                return View(admin);
            }
        }
    }
}
