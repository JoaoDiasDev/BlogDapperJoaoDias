using BlogDapperJoaoDias.Entities;
using BlogDapperJoaoDias.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogDapperJoaoDias.Controllers
{
    public class CommentController : Controller
    {
        private CommentService _commentService;
        private ArticleService _articleService;

        public CommentController(IServiceProvider serviceProvider)
        {
            _commentService = serviceProvider.GetRequiredService<CommentService>();
            _articleService = serviceProvider.GetRequiredService<ArticleService>();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add(string id, IFormCollection form)
        {
            var article = _articleService.GetByGuid(id);
            var comment = new Comment
            {
                ArticleId = article.ArticleId,
                CommentText = form["message"],
                Email = form["email"],
                Name = form["name"],
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                Status = 1
            };

            var result = _commentService.Add(comment);
            if (result > 0)
            {
                return Ok(new { success = "true" });
            }
            else
            {
                return Ok(new { success = "false" });
            }
        }
    }
}
