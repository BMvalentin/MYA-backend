using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;

namespace SistemasCafeBackEnd.Repositories
{
    public class Repository
    {
        private string dbConexion = "Server=localhost;Port=3306;Database=MYA;Uid=root;Pwd=";
        public Repository() { }
        
        public async Task<dynamic> ExecuteProcedure(string procedureName, DynamicParameters parameters)
        {
            using (SqlConnection connection = new SqlConnection(dbConexion))
            {
                try
                {
                    return await connection.ExecuteAsync(procedureName, parameters, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<List<T>> GetListFromProcedure<T>(string procedureName, DynamicParameters parameters)
        {
            using (SqlConnection connection = new SqlConnection(dbConexion))
            {
                IEnumerable<T> rows = await connection.QueryAsync<T>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                return rows.AsList();
            }
        }

        public async Task<List<T>> GetListFromProcedure<T>(string procedureName)
        {
            using (SqlConnection connection = new SqlConnection(dbConexion))
            {
                IEnumerable<T> rows = await connection.QueryAsync<T>(procedureName, commandType: CommandType.StoredProcedure);
                return rows.AsList();
            }
        }
    }
}