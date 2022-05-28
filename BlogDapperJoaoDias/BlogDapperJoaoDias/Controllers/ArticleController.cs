using BlogDapperJoaoDias.Entities;
using BlogDapperJoaoDias.Models;
using BlogDapperJoaoDias.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogDapperJoaoDias.Controllers
{
    public class ArticleController : Controller
    {
        private CategoryService _categoryService;
        private CityService _cityService;

        public ArticleController(IServiceProvider serviceProvider)
        {
            _categoryService = serviceProvider.GetRequiredService<CategoryService>();
            _cityService = serviceProvider.GetRequiredService<CityService>();
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
            if (!ModelState.IsValid)
            {
                var message = string.Join("|", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
            };
            var guid = Guid.NewGuid();
            model.Guid = guid.ToString();
            model.CreatedDate = DateTime.Now;
            model.ModifiedDate = DateTime.Now;
            model.PublishingDate = DateTime.Now;
            return View();
        }
    }
}
