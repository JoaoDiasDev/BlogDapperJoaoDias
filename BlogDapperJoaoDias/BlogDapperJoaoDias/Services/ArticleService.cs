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

        public int GetCount(int categoryId)
        {
            return Convert.ToInt32(_connection.ExecuteScalar("select count(CategoryId) from Articles where CategoryId =" + categoryId).ToString());
        }

        internal List<Article> GetHome()
        {
            var articles = new List<Article>();
            try
            {
                //articles = _connection.Query<Article>("select * from Articles where HomeView = 1 and Status = 1 or Slider = 1").ToList();
                string sql = @"select * from Articles
                              inner join Categorys as cat ON cat.CategoryId = Articles.CategoryId
                              where HomeView = 1 and Status = 1 and Slider = 1";
                articles = _connection.Query<Article, Category, Article>(sql, (article, category) =>
                {
                    article.Category = category;
                    return article;

                }, splitOn: "CategoryId").ToList();
            }
            catch (Exception error)
            {

                throw new ArgumentException(error.Message);
            }

            return articles;
        }

        public List<Article> Search(string q)
        {
            var articles = new List<Article>();
            try
            {
                articles = _connection.Query<Article>(@"select * from Articles where Status = 1 and Articles.Title like @q 
                            or Articles.Content like @q order by ArticleId desc", new { q = "%" + q + "%" }).Take(5).ToList();
            }
            catch (Exception e)
            {

                throw new ArgumentException(e.Message);
            }
            return articles;
        }

        public List<Article> GetArticles(int page = 1)
        {
            var articles = new List<Article>();
            try
            {
                var parameters = new DynamicParameters();
                int pagesize = 3;
                int offset = (page - 1) * 3;
                parameters.Add("@offset", offset);
                parameters.Add("@pagesize", pagesize);

                string sql = @"select * from Articles 
                               inner join Categorys as cat ON cat.CategoryId = Articles.CategoryId
                               where HomeView = 1 and Status = 1 or Slider = 1
                               order by PublishDate desc
                               OFFSET @offset ROWS
                               FETCH NEXT @pagesize ROWS ONLY";
                articles = _connection.Query<Article, Category, Article>(sql, (article, category) =>
                {
                    article.Category = category;
                    return article;
                }, parameters, splitOn: "CategoryId").ToList();
            }
            catch (Exception error)
            {

                throw new ArgumentException(error.Message);
            }

            return articles;
        }

        public int CountArticles()
        {
            return Convert.ToInt32(_connection.ExecuteScalar("select count(ArticleId) from Articles where Status = 1").ToString());
        }

        public List<Article> GetCategory(int id, int page = 1)
        {
            var articles = new List<Article>();
            try
            {
                var parameters = new DynamicParameters();
                var pageSize = 3;
                var offset = (page - 1) * pageSize;
                parameters.Add("@id", id);
                parameters.Add("@offset", offset);
                parameters.Add("@pagesize", pageSize);
                string sql = @"select * from Articles
                              where Status = 1
                              and CategoryId = @id
                              order by PublishDate desc
                              OFFSET @offset ROWS
                              FETCH NEXT @pagesize ROWS ONLY";
                articles = _connection.Query<Article>(sql, parameters).ToList();

            }
            catch (Exception error)
            {

                throw new ArgumentException(error.Message);
            }
            return articles;
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

        public Article GetNext(int id)
        {
            var param = new DynamicParameters();
            param.Add("@id", id);
            string sql = @"select top 1 ArticleId, Guid, Title, Image from Articles
                          where ArticleId > @id
                          order by ArticleId asc";
            var article = _connection.Query<Article>(sql, param).FirstOrDefault();
            if (article != null)
            {
                return article;
            }
            else
            {
                return new Article();
            }
        }

        public Article GetPrevious(int id)
        {
            var param = new DynamicParameters();
            param.Add("@id", id);
            string sql = @"select ArticleId, Guid, Title, Image from Articles
                          where ArticleId < @id
                          order by ArticleId desc
                          OFFSET 0 ROWS
                          FETCH NEXT 1 ROWS ONLY";
            var article = _connection.Query<Article>(sql, param).FirstOrDefault();
            if (article != null)
            {
                return article;
            }
            else
            {
                return new Article();
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
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }

            if (article != null)
            {
                return article;
            }
            else
            {
                return new Article();
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
