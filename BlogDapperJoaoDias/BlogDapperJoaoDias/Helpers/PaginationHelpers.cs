using BlogDapperJoaoDias.Entities;
using BlogDapperJoaoDias.Models;
using BlogDapperJoaoDias.Services;

namespace BlogDapperJoaoDias.Helpers
{
    public class PaginationHelpers
    {
        private ArticleService _articleService;
        public PaginationHelpers(IServiceProvider serviceProvider)
        {
            _articleService = serviceProvider.GetRequiredService<ArticleService>();
        }

        public PaginationModel CategoryPagination(Category category, int page)
        {
            var paginationModel = new PaginationModel();
            var totalCount = _articleService.GetCount(category.CategoryId);
            paginationModel.TotalCount = totalCount;

            var pageSize = Math.Ceiling(decimal.Parse(totalCount.ToString()) / 3);
            var pageCount = (int)Math.Round(pageSize);
            paginationModel.PageCount = pageCount;

            var articles = _articleService.GetCategory(category.CategoryId);
            paginationModel.ArticlesList = articles;
            var pageHtml = "";

            if (pageCount > 1)
            {
                for (int i = 1; i < pageCount + 1; i++)
                {
                    var active = i == page ? "active" : "";
                    pageHtml += "<li class='page-item " + active + "'><a href='/Category/" + category.Slug + "/" + i + "' class='page-link'>" + i + "</a></li>";
                }
            }
            var html = $@"<nav class='blog-pagination justify-content-center d-flex'>
                               <ul class='pagination'>
                                  {pageHtml}
                               </ul>
                             </nav>";

            paginationModel.Html = html;
            return paginationModel;
        }
    }
}
