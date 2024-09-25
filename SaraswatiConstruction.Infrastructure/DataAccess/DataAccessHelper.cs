using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SaraswatiConstruction.Infrastructure.DataAccess.Interface;
using System.Data;

namespace SaraswatiConstruction.Infrastructure.DataAccess
{
    public class DataAccessHelper : IDataAccessHelper
    {
        private readonly IConfiguration _configuration;
        private readonly string? _connectionString;

        public DataAccessHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlConnection");
        }

        public IDbConnection CreateConnection()
        {

            return new SqlConnection(_connectionString);
        }

        public async Task<IEnumerable<T>> ExecuteStoredProcedureDataTableAsync<T>(string procedureName)
        {
            using (var connection = CreateConnection())
            {

                var result = await connection.QueryAsync<T>(procedureName, commandType: CommandType.StoredProcedure);

                return result;
            }
        }

        public async Task<IEnumerable<T>> ExecuteStoredProcedureDataTableAsync<T>(string procedureName, object parameters)
        {
            using (var connection = CreateConnection())
            {
                return await connection.QueryAsync<T>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<List<IEnumerable<T>>> ExecuteStoredProcedureDataSetAsync<T>(string procedureName)
        {
            using (var connection = CreateConnection())
            {
                using (var multi = await connection.QueryMultipleAsync(procedureName, commandType: CommandType.StoredProcedure))
                {
                    var resultSets = new List<IEnumerable<T>>();

                    while (!multi.IsConsumed)
                    {
                        var resultSet = await multi.ReadAsync<T>();
                        resultSets.Add(resultSet);
                    }
                    return resultSets;
                }
            }
        }

        public async Task<List<IEnumerable<T>>> ExecuteStoredProcedureDataSetAsync<T>(string procedureName, object parameters)
        {
            using (var connection = CreateConnection())
            {
                using (var multi = await connection.QueryMultipleAsync(procedureName, parameters, commandType: CommandType.StoredProcedure))
                {
                    var resultSets = new List<IEnumerable<T>>();

                    while (!multi.IsConsumed)
                    {
                        var resultSet = await multi.ReadAsync<T>();
                        resultSets.Add(resultSet);
                    }

                    return resultSets;
                }
            }
        }

    }

}
