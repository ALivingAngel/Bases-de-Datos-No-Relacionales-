using DistribuidoraWalter.Data;
using DistribuidoraWalter.Model;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace WebApiDatas.Repository
{
    public class LoteRepository
    {
        public async Task<List<LoteModel>> GetAllLotesAsync()
        {
            var lista = new List<LoteModel>();
            var connectionString = DBConnection.Connect();

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand("[dbo].[USP_GetAllLote]", connection);
            command.CommandType = CommandType.StoredProcedure;

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(new LoteModel
                {
                    IdLote = reader.GetInt32(reader.GetOrdinal("IdLote")),
                    Codigo = reader.GetString(reader.GetOrdinal("Codigo")),
                    IdProducto = reader.GetInt32(reader.GetOrdinal("IdProducto")),
                    NombreProducto = reader.GetString(reader.GetOrdinal("NombreProducto")),
                    FechaFabricacion = reader.IsDBNull(reader.GetOrdinal("FechaFabricacion"))
                        ? null
                        : reader.GetDateTime(reader.GetOrdinal("FechaFabricacion")),
                    FechaCaducidad = reader.IsDBNull(reader.GetOrdinal("FechaCaducidad"))
                        ? null
                        : reader.GetDateTime(reader.GetOrdinal("FechaCaducidad")),
                    IdProveedor = reader.GetInt32(reader.GetOrdinal("IdProveedor")),
                    NombreProveedor = reader.GetString(reader.GetOrdinal("NombreProveedor")),
                    Observacion = reader.IsDBNull(reader.GetOrdinal("Observacion"))
                        ? null
                        : reader.GetString(reader.GetOrdinal("Observacion"))
                });
            }

            return lista;
        }
    }
}
