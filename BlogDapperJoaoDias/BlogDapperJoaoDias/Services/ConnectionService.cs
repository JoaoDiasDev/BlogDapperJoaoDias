using System.Data;
using System.Data.SqlClient;

namespace BlogDapperJoaoDias.Services
{
    public class ConnectionService
    {
        private SqlConnection _myConnection;
        private IConfiguration _configuration;

        public ConnectionService(IConfiguration configuration)
        {
            _configuration = configuration;
            var connectionString = _configuration.GetConnectionString("DefaultConnection").ToString();
            _myConnection = new SqlConnection(connectionString);
            if (_myConnection.State != ConnectionState.Open)
            {
                _myConnection.Open();
            }
        }

        internal SqlConnection DbConnection()
        {
            return _myConnection;
        }

        internal SqlConnection ForDapper()
        {
            var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection").ToString());
            var state = connection.State;
            if (state != ConnectionState.Open)
            {
                connection.Open();
            }

            return connection;
        }
    }
}
