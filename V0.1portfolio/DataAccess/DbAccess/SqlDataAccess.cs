using Dapper;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DataAccess.DbAccess;


/// <summary>
/// Connection to DB using a connectionstring or default connectionstring - Can be set in appsettings.json
/// Load or Save data using a Storedprocedure with parameters [Id, FirstName, LastName]
/// </summary>


public class SqlDataAccess : ISqlDataAccess
{
    private readonly IConfiguration _config;

    public SqlDataAccess(IConfiguration config)
    {
        _config = config;
    }

    public async Task<IEnumerable<T>> LoadData<T, U>(
        string storedProcedure,
        U parameters,
        string connectionId = "Default")
    {

        // Connect to SQL
        using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

        // Run stored procedure and return IEnumerable model
        return await connection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task SaveData<T>(
        string storedProcedure,
        T parameters,
        string connectionId = "Default")
    {
        // Connect to SQL
        using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

        // Execute stored proceudre og commandtype: storedprocedure
        await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
    }
}
