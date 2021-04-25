using System;
using System.Data;
using System.Threading.Tasks;
using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Interfaces;
using MySql.Data.MySqlClient;

namespace DeveloperManagement.WorkItemManagement.Infrastructure.Persistence
{
    public class DapperConnectionFactory : IDapperConnectionFactory
    {
        private readonly string _connectionString;

        public DapperConnectionFactory(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public async Task<IDbConnection> CreateConnectionAsync()
        {
            var sqlConnection = new MySqlConnection(_connectionString);
            await sqlConnection.OpenAsync();
            return sqlConnection;
        }
    }
}