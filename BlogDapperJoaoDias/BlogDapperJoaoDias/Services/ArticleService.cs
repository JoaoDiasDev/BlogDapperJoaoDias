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
    }
}
