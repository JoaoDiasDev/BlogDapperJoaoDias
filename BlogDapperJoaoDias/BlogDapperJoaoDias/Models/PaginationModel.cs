using BlogDapperJoaoDias.Entities;

namespace BlogDapperJoaoDias.Models
{

    public class PaginationModel
    {
        public PaginationModel()
        {
            ArticlesList = new List<Article>();
            Html = "";
        }

        public int TotalCount;
        public List<Article> ArticlesList;
        public string Html;
        public int PageCount;
    }
}
