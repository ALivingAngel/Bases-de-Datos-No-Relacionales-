using DistribuidoraWalter.Data;
using DistribuidoraWalter.Model;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace WebApiDatas.Repository
{
    public class InventarioRepository
    {
        public async Task<List<InventarioModel>> GetAllInventarioAsync()
        {
            var result = new List<InventarioModel>();
            var connectionString = DBConnection.Connect();

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand("Inventario.ObtenerTodoInventario", connection);
            command.CommandType = CommandType.StoredProcedure;

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                result.Add(new InventarioModel
                {
                    IdProducto = reader.GetInt32(reader.GetOrdinal("IdProducto")),
                    NombreProducto = reader.GetString(reader.GetOrdinal("NombreProducto")),

                    IdBodega = reader.GetInt32(reader.GetOrdinal("IdBodega")),
                    NombreBodega = reader.GetString(reader.GetOrdinal("NombreBodega")),

                    Stock = reader.GetDecimal(reader.GetOrdinal("Stock")),
                    AbreviaturaUnidad = reader.GetString(reader.GetOrdinal("AbreviaturaUnidad"))
                });
            }

            return result;
        }


        public async Task<List<InventarioLoteModel>> GetAllInventarioLoteAsync()
        {
            var result = new List<InventarioLoteModel>();
            var connectionString = DBConnection.Connect();

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand("Inventario.ObtenerTodoInventarioLote", connection);
            command.CommandType = CommandType.StoredProcedure;

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                result.Add(new InventarioLoteModel
                {
                    IdProducto = reader.GetInt32(reader.GetOrdinal("IdProducto")),
                    NombreProducto = reader.GetString(reader.GetOrdinal("NombreProducto")),

                    IdBodega = reader.GetInt32(reader.GetOrdinal("IdBodega")),
                    NombreBodega = reader.GetString(reader.GetOrdinal("NombreBodega")),

                    IdLote = reader.GetInt32(reader.GetOrdinal("IdLote")),
                    CodigoLote = reader.GetString(reader.GetOrdinal("CodigoLote")),

                    Stock = reader.GetDecimal(reader.GetOrdinal("Stock")),
                    AbreviaturaUnidad = reader.GetString(reader.GetOrdinal("AbreviaturaUnidad"))
                });
            }

            return result;
        }
    }
}

namespace DistribuidoraWalter.Model
{
    public class InventarioModel
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; } = string.Empty;

        public int IdBodega { get; set; }
        public string NombreBodega { get; set; } = string.Empty;

        public decimal Stock { get; set; }
        public string AbreviaturaUnidad { get; set; } = string.Empty;
    }
}

