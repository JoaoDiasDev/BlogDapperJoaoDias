using BlogDapperJoaoDias.Entities;
using BlogDapperJoaoDias.Helpers;
using BlogDapperJoaoDias.Models;
using BlogDapperJoaoDias.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogDapperJoaoDias.Controllers
{
    public class ArticleController : Controller
    {
        private CategoryService _categoryService;
        private CityService _cityService;
        private IWebHostEnvironment _hostingEnvironment;

        public ArticleController(IServiceProvider serviceProvider)
        {
            _categoryService = serviceProvider.GetRequiredService<CategoryService>();
            _cityService = serviceProvider.GetRequiredService<CityService>();
            _hostingEnvironment = serviceProvider.GetRequiredService<IWebHostEnvironment>();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            var categories = _categoryService.GetAll();
            var cities = _cityService.GetAll();
            var model = new GeneralViewModel
            {
                CategoryList = categories,
                CityList = cities

            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Save(Article model, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                var guid = Guid.NewGuid();
                model.Guid = guid.ToString();
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.PublishingDate = DateTime.Now;
                if (file.Length > 0)
                {
                    var uploadHelper = new UploadHelpers(_hostingEnvironment);
                    var fileName = await uploadHelper.Upload(file);
                    if (!string.IsNullOrEmpty(fileName))
                    {
                        model.Image = fileName;
                    }
                }

            }
            else
            {
                var message = string.Join("|", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                return View(message);
            }

            return View();
        }
    }
}
