using BlogDapperJoaoDias.Entities;
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
            return result > 0 ? int.Parse(result.ToString()) : 0;
        }
    }
}
