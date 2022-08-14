using BlogDapperJoaoDias.Models;
using BlogDapperJoaoDias.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogDapperJoaoDias.ViewComponents
{
    public class RightSide : ViewComponent
    {
        private CategoryService _categoryService;
        private ArticleService _articleService;

        public RightSide(IServiceProvider serviceProvider)
        {
            _categoryService = serviceProvider.GetRequiredService<CategoryService>();
            _articleService = serviceProvider.GetRequiredService<ArticleService>();
        }

        public IViewComponentResult Invoke()
        {
            var categories = _categoryService.GetAll();
            var articles = _articleService.GetAll().Take(4).OrderByDescending(art => art.Hit).ToList();

            var model = new GeneralViewModel
            {
                CategoryList = categories,
                ArticleList = articles
            };

            return View(model);
        }
    }
}
