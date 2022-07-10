using BlogDapperJoaoDias.Entities;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Connections;
using System.Data;
using System.Data.SqlClient;

namespace BlogDapperJoaoDias.Services
{
    public class CategoryService
    {
        private SqlConnection _adoSqlConnection;
        private IDbConnection _connection;
        private ConnectionService _connectionService;

        public CategoryService(IConfiguration configuration)
        {
            _connectionService = new ConnectionService(configuration);
            _adoSqlConnection = _connectionService.DbConnection();
            _connection = _connectionService.ForDapper();
        }

        public List<Category> GetAllAdoNet()
        {
            var categories = new List<Category>();
            var command = new SqlCommand("select * from Category", _adoSqlConnection);
            command.CommandType = CommandType.Text;
            var dataReader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (dataReader.Read())
            {
                var category = new Category();
                category.CategoryId = dataReader["CategoryId"] is DBNull
                    ? 0
                    : int.Parse(dataReader["CategoryId"].ToString() ?? string.Empty);
                category.CategoryName = (dataReader["CategoryName"] is DBNull
                    ? string.Empty
                    : dataReader["CategoryName"].ToString()) ?? string.Empty;
                category.Slug = (dataReader["Slug"] is DBNull
                    ? string.Empty
                    : dataReader["Slug"].ToString()) ?? string.Empty;
                category.OrderBy = dataReader["OrderBy"] is DBNull
                    ? 0
                    : int.Parse(dataReader["OrderBy"].ToString() ?? string.Empty);
                categories.Add(category);
            }

            return categories;
        }

        public List<Category> GetAll()
        {
            List<Category> categories;
            try
            {
                categories = _connection.Query<Category>(@"select * from Categorys").ToList();
            }
            catch (Exception e)
            {
                throw new ConnectionAbortedException(e.Message);
            }

            return categories;
        }

        public int Add(Category category)
        {
            var result = _connection.Insert(category);
            return Convert.ToInt32(result);
        }
    }
}
