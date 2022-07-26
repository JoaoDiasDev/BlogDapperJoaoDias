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

        [HttpPost, AutoValidateAntiforgeryToken]
        public IActionResult Index(Entities.Admin admin)
        {
            if (admin.Username != "" || admin.Password != "")
            {
                Entities.Admin myAdmin = adminService.Login(admin);
                if (myAdmin == null)
                {
                    ViewBag.Error = "Something went wrong please try it again!";
                    return View(admin);
                }
                else
                {
                    var userIdCookie = new CookieOptions();
                    userIdCookie.Expires = DateTime.Now.AddDays(3);
                    Response.Cookies.Append("userid", myAdmin.AdminId.ToString(), userIdCookie);
                    var usernameCookie = new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(3),
                    };
                    Response.Cookies.Append("username", myAdmin.Username, usernameCookie);
                    return RedirectToAction("Index", "Admin");
                }
            }
            else
            {
                ViewBag.Error = "Please check your username or password";
                return View(admin);
            }
        }
        [Route("/Admin/Login/Logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            if (HttpContext.Request.Cookies.Count > 0)
            {
                foreach (var item in HttpContext.Request.Cookies.Keys)
                {
                    Response.Cookies.Delete(item);
                }
            }
            return Redirect("/");
        }
    }
}
