using BlogDapperJoaoDias.Entities;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Data;

namespace BlogDapperJoaoDias.Services
{
    public class ArticleService
    {
        private IDbConnection _connection;
        private ConnectionService _connectionService;

        public ArticleService(IConfiguration configuration)
        {
            _connectionService = new ConnectionService(configuration);
            _connection = _connectionService.ForDapper();
        }

        public int Add(Article article)
        {
            var result = _connection.Insert(article);
            if (result > 0)
            {
                return int.Parse(result.ToString());
            }

            return 0;
        }

        public List<Article> GetAll()
        {
            var articles = new List<Article>();
            try
            {
                articles = _connection.Query<Article>(@"select * from Articles").ToList();
            }
            catch (Exception error)
            {

                throw new ArgumentException(error.Message);
            }
            return articles;
        }

        public Article GetById(int id)
        {
            var article = new Article();
            try
            {
                article = _connection.Query<Article>(@"select * from Articles where ArticleId = " + id)
                    .FirstOrDefault();
                return article;
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }
        public Article GetByGuid(string guid)
        {
            var article = new Article();
            try
            {
                var param = new DynamicParameters();
                param.Add("@guid", guid);
                article = _connection.Query<Article>(@"select * from Articles where Guid = @guid ", param).FirstOrDefault();
                return article;
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }

        public bool Delete(Article article)
        {
            try
            {
                bool result = _connection.Delete(article);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public Article Update(Article article)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@id", article.ArticleId);
            parameters.Add("@Title", article.Title);
            parameters.Add("@category", article.CategoryId);
            parameters.Add("@city", article.CityId);
            parameters.Add("@status", article.Status);
            parameters.Add("@homeview", article.HomeView);
            parameters.Add("@slider", article.Slider);
            parameters.Add("@seen", 1);
            parameters.Add("@publishDate", DateTime.Now);
            try
            {
                _connection.Execute("update Articles set Title=@title,CategoryId=@category,CityId=@city,Status=@status,HomeView=@homeview,Slider=@slider,Seen=@seen,PublishDate=@publishDate where ArticleId=@id", parameters);
                return article;
            }
            catch (Exception)
            {
                return new Article();
            }
        }

        public List<Article> GetStatus(int status)
        {
            var articles = new List<Article>();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@status", status);
                articles = _connection.Query<Article>(@"select * from Articles where Status = @status", parameters).ToList();
            }
            catch (Exception error)
            {
                throw new ArgumentException(error.Message);
            }
            return articles;
        }
    }
}
