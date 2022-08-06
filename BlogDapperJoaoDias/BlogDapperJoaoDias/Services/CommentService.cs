using BlogDapperJoaoDias.Entities;
using Dapper.Contrib.Extensions;
using System.Data;

namespace BlogDapperJoaoDias.Services
{
    public class CommentService
    {
        private IDbConnection _connection;
        private ConnectionService _connectionService;

        public CommentService(IConfiguration configuration)
        {
            _connectionService = new ConnectionService(configuration);
            _connection = _connectionService.ForDapper();
        }

        public int Add(Comment comment)
        {
            try
            {
                var result = _connection.Insert(comment);
                if (result > 0)
                {
                    return int.Parse(result.ToString());
                }

            }
            catch (Exception)
            {

                return 0;
            }
        }
    }
}
