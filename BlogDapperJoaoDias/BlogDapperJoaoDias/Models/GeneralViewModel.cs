using BlogDapperJoaoDias.Entities;

namespace BlogDapperJoaoDias.Models
{
    public class GeneralViewModel
    {
        public GeneralViewModel()
        {
            Category = new Category();
            Article = new ArticleViewModel();
            CategoryList = new List<Category>();
            ArticleList = new List<ArticleViewModel>();
            CityList = new List<City>();
            PeviousArticle = new ArticleViewModel();
            NextArticle = new ArticleViewModel();
            PaginationModel = new PaginationModel();
        }
        public Category Category { get; set; }
        public ArticleViewModel Article { get; set; }
        public List<Category> CategoryList { get; set; }
        public List<ArticleViewModel> ArticleList { get; set; }
        public List<City> CityList { get; set; }
        public ArticleViewModel PeviousArticle { get; set; }
        public ArticleViewModel NextArticle { get; set; }
        public PaginationModel PaginationModel { get; set; }
    }
}
