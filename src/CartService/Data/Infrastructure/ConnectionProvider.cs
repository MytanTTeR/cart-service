using System.Data;

using CartService.Data.Configs;

using Microsoft.Data.SqlClient;

namespace CartService.Data.Infrastructure
{
    internal class ConnectionProvider : IConnectionProvider
    {
        private readonly CartDbConfig _dbConfig;

        public ConnectionProvider(CartDbConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_dbConfig.ConnectionString);
        }
    }
}
