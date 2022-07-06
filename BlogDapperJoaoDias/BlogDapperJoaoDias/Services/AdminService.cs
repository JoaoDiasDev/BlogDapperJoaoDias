using BlogDapperJoaoDias.Entities;
using Dapper;
using Dapper.Contrib.Extensions;
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
                myAdmin = _connection.Query<Admin>($@"select AdminId, Username, Password from Admins where Username = @username and Password = @password ", parameters).FirstOrDefault();
            }
            catch (Exception e)
            {

                throw new ArgumentException(e.Message);
            }

            return myAdmin;
        }

        public List<Admin> GetAll()
        {
            var userList = new List<Admin>();

            try
            {
                userList = _connection.Query<Admin>("select * from Admins").ToList();
            }
            catch (Exception e)
            {

                throw new ArgumentException(e.Message);
            }
            return userList;
        }

        public int Add(Admin admin)
        {
            var result = _connection.Insert(admin);
            return Convert.ToInt32(result);
        }

        public Admin Get(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            var myAdmin = _connection.Query<Admin>($@"select * from Admins where AdminId=@id", parameters).FirstOrDefault();
            return myAdmin;
        }

        public Admin Update(Admin admin)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@id", admin.AdminId);
            parameters.Add("@username", admin.Username);
            parameters.Add("@password", admin.Password);
            try
            {
                _connection.Execute("update Admins set Username=@username, Password=@password where AdminId=@id", parameters);
                return admin;
            }
            catch (Exception)
            {
                return new Admin();
            }
        }

        public bool Delete(Admin admin)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@id", admin.AdminId);
            try
            {
                _connection.Execute("delete from Admins where AdminId=@id", parameters);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
