using BlogDapperJoaoDias.Entities;
using Dapper;
using System.Data;

namespace BlogDapperJoaoDias.Services
{
    public class AdminService
    {
        private IDbConnection _connection;
        private ConnectionService _connectionService;

        public AdminService(IConfiguration configuration)
        {
            _connectionService = new ConnectionService(configuration);
            _connection = _connectionService.ForDapper();
        }

        public Admin Login(Admin admin)
        {
            var myAdmin = new Admin();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@username", admin.Username);
                parameters.Add("@password", admin.Password);
                myAdmin = _connection.Query<Admin>($@"select id as AdminId, Username, Password from Admins where Username = @username and Password = @password ", parameters).FirstOrDefault();
            }
            catch (Exception e)
            {

                throw new ArgumentException(e.Message);
            }

            return myAdmin;
        }
    }
}
