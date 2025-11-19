using DistribuidoraWalter.Data;
using DistribuidoraWalter.Model;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace WebApiDatas.Repository
{
    public class DevolucionCompraRepository
    {
        public async Task<List<DevolucionCompraModel>> GetAllDevolucionesCompraAsync()
        {
            var devoluciones = new List<DevolucionCompraModel>();
            var connectionString = DBConnection.Connect();

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand("Transaccion.ObtenerTodasDevolucionesCompra", connection);
            command.CommandType = CommandType.StoredProcedure;

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var devolucion = new DevolucionCompraModel
                {
                    IdDevolucionCompra = reader.GetInt32(reader.GetOrdinal("IdDevolucionCompra")),
                    IdCompra = reader.GetInt32(reader.GetOrdinal("IdCompra")),
                    Fecha = reader.GetDateTime(reader.GetOrdinal("Fecha")),
                    Motivo = reader.IsDBNull(reader.GetOrdinal("Motivo")) ? null : reader.GetString(reader.GetOrdinal("Motivo")),
                    UsuarioRegistro = reader.IsDBNull(reader.GetOrdinal("UsuarioRegistro")) ? null : reader.GetString(reader.GetOrdinal("UsuarioRegistro")),
                    Detalles = new List<DevolucionCompraDetalleModel>() // Vacío, solo cabecera
                };
                devoluciones.Add(devolucion);
            }

            return devoluciones;
        }

        public async Task<DevolucionCompraModel?> GetDevolucionCompraAsync(int idDevolucionCompra)
        {
            var connectionString = DBConnection.Connect();
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand("Transaccion.ObtenerDevolucionCompra", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@IdDevolucionCompra", idDevolucionCompra);

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();

            // Leer la cabecera
            if (!await reader.ReadAsync())
                return null; // No existe

            var devolucion = new DevolucionCompraModel
            {
                IdDevolucionCompra = reader.GetInt32(reader.GetOrdinal("IdDevolucionCompra")),
                IdCompra = reader.GetInt32(reader.GetOrdinal("IdCompra")),
                Fecha = reader.GetDateTime(reader.GetOrdinal("Fecha")),
                Motivo = reader.IsDBNull(reader.GetOrdinal("Motivo")) ? null : reader.GetString(reader.GetOrdinal("Motivo")),
                UsuarioRegistro = reader.IsDBNull(reader.GetOrdinal("UsuarioRegistro")) ? null : reader.GetString(reader.GetOrdinal("UsuarioRegistro"))
            };

            // Pasar al siguiente resultado para detalles
            if (await reader.NextResultAsync())
            {
                while (await reader.ReadAsync())
                {
                    var detalle = new DevolucionCompraDetalleModel
                    {
                        IdProducto = reader.GetInt32(reader.GetOrdinal("IdProducto")),
                        NombreProducto = reader.GetString(reader.GetOrdinal("NombreProducto")),
                        IdBodega = reader.GetInt32(reader.GetOrdinal("IdBodega")),
                        NombreBodega = reader.GetString(reader.GetOrdinal("NombreBodega")),
                        IdLote = reader.GetInt32(reader.GetOrdinal("IdLote")),
                        Cantidad = reader.GetDecimal(reader.GetOrdinal("Cantidad")),
                        UnidadMedida = reader.IsDBNull(reader.GetOrdinal("UnidadMedida")) ? null : reader.GetString(reader.GetOrdinal("UnidadMedida"))
                    };
                    devolucion.Detalles.Add(detalle);
                }
            }

            return devolucion;
        }
        public async Task<int> RegistrarDevolucionCompraAsync(DevolucionCompraInputModel input)
        {
            var connectionString = DBConnection.Connect();

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand("Transaccion.RegistrarDevolucionCompra", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@IdCompra", input.IdCompra);
            command.Parameters.AddWithValue("@Motivo", (object?)input.Motivo ?? DBNull.Value);
            command.Parameters.AddWithValue("@UsuarioRegistro", (object?)input.UsuarioRegistro ?? DBNull.Value);

            var outputIdParam = new SqlParameter("@IdDevolucionCompra", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(outputIdParam);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();

            return (int)outputIdParam.Value;
        }
    }
}
