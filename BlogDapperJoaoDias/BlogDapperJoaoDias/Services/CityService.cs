using BlogDapperJoaoDias.Entities;
using Dapper;
using Microsoft.AspNetCore.Connections;
using System.Data;

namespace BlogDapperJoaoDias.Services
{
    public class CityService
    {
        private IDbConnection _dapperConnection;
        private ConnectionService _connectionService;

        public CityService(IConfiguration configuration)
        {
            _connectionService = new ConnectionService(configuration);
            _dapperConnection = _connectionService.ForDapper();
        }

        public List<City> GetAll()
        {
            List<City> cities;
            try
            {
                cities = _dapperConnection.Query<City>(@"select * from Citys").ToList();
            }
            catch (Exception e)
            {
                throw new ConnectionAbortedException(e.Message);
            }

            return cities;
        }
    }
}
