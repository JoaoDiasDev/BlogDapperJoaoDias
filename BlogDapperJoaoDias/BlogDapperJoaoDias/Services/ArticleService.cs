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
    }
}
