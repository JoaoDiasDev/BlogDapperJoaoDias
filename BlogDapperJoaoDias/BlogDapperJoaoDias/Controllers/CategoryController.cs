using BlogDapperJoaoDias.Helpers;
using BlogDapperJoaoDias.Models;
using BlogDapperJoaoDias.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogDapperJoaoDias.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryService _categoryService;
        private ArticleService _articleService;
        private IServiceProvider _service;
        public CategoryController(IServiceProvider serviceProvider)
        {
            _categoryService = serviceProvider.GetRequiredService<CategoryService>();
            _articleService = serviceProvider.GetRequiredService<ArticleService>();
            _service = serviceProvider;
        }

        [Route("/Category/{slug}/{page?}")]
        public IActionResult Index(string slug, int page = 1)
        {
            if (slug != null)
            {
                var category = _categoryService.GetSlug(slug);
                if (category != null)
                {
                    var categories = _categoryService.GetAll();
                    var paginationHelper = new PaginationHelpers(_service);
                    var paginationModel = paginationHelper.CategoryPagination(category, page);

                    var model = new GeneralViewModel
                    {
                        CategoryList = categories,
                        PaginationModel = paginationModel,
                        Category = category
                    };
                    return View(model);
                }
                else
                {
                    return Redirect("/");
                }
            }
            else
            {
                return Redirect("/");
            }
        }
    }
}
