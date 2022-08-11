using BlogDapperJoaoDias.Entities;

namespace BlogDapperJoaoDias.Models
{
    public class GeneralViewModel
    {
        public GeneralViewModel()
        {
            Category = new Category();
            Article = new Article();
            CategoryList = new List<Category>();
            ArticleList = new List<Article>();
            CityList = new List<City>();
            PeviousArticle = new Article();
            NextArticle = new Article();
            PaginationModel = new PaginationModel();
            Comments = new List<Comment>();
        }
        public Category Category { get; set; }
        public Article Article { get; set; }
        public List<Category> CategoryList { get; set; }
        public List<Article> ArticleList { get; set; }
        public List<City> CityList { get; set; }
        public Article PeviousArticle { get; set; }
        public Article NextArticle { get; set; }
        public PaginationModel PaginationModel { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
