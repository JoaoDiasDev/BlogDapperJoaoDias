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
        private ArticleService _articleService;
        private CommentService _commentService;

        public ArticleController(IServiceProvider serviceProvider)
        {
            _categoryService = serviceProvider.GetRequiredService<CategoryService>();
            _cityService = serviceProvider.GetRequiredService<CityService>();
            _hostingEnvironment = serviceProvider.GetRequiredService<IWebHostEnvironment>();
            _articleService = serviceProvider.GetRequiredService<ArticleService>();
            _commentService = serviceProvider.GetRequiredService<CommentService>();
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(Article model, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                var guid = Guid.NewGuid();
                model.Guid = guid.ToString();
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.PublishDate = DateTime.Now;
                if (file.Length > 0)
                {
                    var uploadHelper = new UploadHelpers(_hostingEnvironment);
                    var fileName = await uploadHelper.Upload(file);
                    if (!string.IsNullOrEmpty(fileName))
                    {
                        model.Image = fileName;
                    }
                }

                var result = _articleService.Add(model);

                if (result > 0)
                {
                    var article = _articleService.GetById(result);
                    return RedirectToAction("Detail", new { id = article.Guid });
                }
                else
                {
                    var message = "Something went wrong please check it!";
                    return View(message);
                }
            }
            else
            {
                var message = string.Join("|", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                return View(message);
            }
        }

        public IActionResult Detail(string id)
        {
            var article = _articleService.GetByGuid(id);
            var previousArticle = _articleService.GetPrevious(article.ArticleId);
            var nextArticle = _articleService.GetNext(article.ArticleId);
            var comments = _commentService.GetByArticle(article.ArticleId);
            var model = new GeneralViewModel
            {
                Article = article,
                PeviousArticle = previousArticle,
                NextArticle = nextArticle,
                Comments = comments
            };
            return View(model);
        }
    }
}
