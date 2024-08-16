using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;

namespace MYABackend.Repositories
{
    public class Repository
    {
        private string dbConexion = "workstation id=MYADataBase.mssql.somee.com;packet size=4096;user id=LuchoCeles_SQLLogin_1;pwd=x5ivqojpfh;data source=MYADataBase.mssql.somee.com;persist security info=False;initial catalog=MYADataBase;TrustServerCertificate=True";

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