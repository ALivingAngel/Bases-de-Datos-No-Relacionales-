using DistribuidoraWalter.Data;
using DistribuidoraWalter.Model;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace WebApiDatas.Repository
{
    public class DevolucionVentaRepository
    {
        public async Task<int> RegistrarDevolucionVentaAsync(DevolucionVentaInputModel input)
        {
            var connectionString = DBConnection.Connect();

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand("Transaccion.RegistrarDevolucionVenta", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@IdVenta", input.IdVenta);
            command.Parameters.AddWithValue("@Motivo", (object?)input.Motivo ?? DBNull.Value);
            command.Parameters.AddWithValue("@UsuarioRegistro", (object?)input.UsuarioRegistro ?? DBNull.Value);

            var outputIdParam = new SqlParameter("@IdDevolucionVenta", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(outputIdParam);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();

            return (int)outputIdParam.Value;
        }
        public async Task<List<DevolucionVentaModel>> GetAllDevolucionesVentaAsync()
        {
            var devoluciones = new List<DevolucionVentaModel>();
            var connectionString = DBConnection.Connect();

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand("Transaccion.ObtenerTodasDevolucionesVenta", connection);
            command.CommandType = CommandType.StoredProcedure;

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var devolucion = new DevolucionVentaModel
                {
                    IdDevolucionVenta = reader.GetInt32(reader.GetOrdinal("IdDevolucionVenta")),
                    IdVenta = reader.GetInt32(reader.GetOrdinal("IdVenta")),
                    Fecha = reader.GetDateTime(reader.GetOrdinal("Fecha")),
                    Motivo = reader.IsDBNull(reader.GetOrdinal("Motivo")) ? null : reader.GetString(reader.GetOrdinal("Motivo")),
                    UsuarioRegistro = reader.IsDBNull(reader.GetOrdinal("UsuarioRegistro")) ? null : reader.GetString(reader.GetOrdinal("UsuarioRegistro")),
                    Detalles = new List<DevolucionVentaDetalleModel>()
                };
                devoluciones.Add(devolucion);
            }

            return devoluciones;
        }


        public async Task<DevolucionVentaModel?> GetDevolucionVentaAsync(int idDevolucionVenta)
        {
            var connectionString = DBConnection.Connect();
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand("Transaccion.ObtenerDevolucionVenta", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@IdDevolucionVenta", idDevolucionVenta);

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();

            // Leer cabecera
            if (!await reader.ReadAsync())
                return null;

            var devolucion = new DevolucionVentaModel
            {
                IdDevolucionVenta = reader.GetInt32(reader.GetOrdinal("IdDevolucionVenta")),
                IdVenta = reader.GetInt32(reader.GetOrdinal("IdVenta")),
                Fecha = reader.GetDateTime(reader.GetOrdinal("Fecha")),
                Motivo = reader.IsDBNull(reader.GetOrdinal("Motivo")) ? null : reader.GetString(reader.GetOrdinal("Motivo")),
                UsuarioRegistro = reader.IsDBNull(reader.GetOrdinal("UsuarioRegistro")) ? null : reader.GetString(reader.GetOrdinal("UsuarioRegistro")),
                Detalles = new List<DevolucionVentaDetalleModel>()
            };

            // Pasar a segundo result set para los detalles
            if (await reader.NextResultAsync())
            {
                while (await reader.ReadAsync())
                {
                    var detalle = new DevolucionVentaDetalleModel
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
    }
    public class DevolucionVentaModel
    {
        public int IdDevolucionVenta { get; set; }
        public int IdVenta { get; set; }
        public DateTime Fecha { get; set; }
        public string? Motivo { get; set; }
        public string? UsuarioRegistro { get; set; }

        public List<DevolucionVentaDetalleModel> Detalles { get; set; } = new List<DevolucionVentaDetalleModel>();
    }
    public class DevolucionVentaDetalleModel
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; } = string.Empty;

        public int IdBodega { get; set; }
        public string NombreBodega { get; set; } = string.Empty;

        public int IdLote { get; set; }

        public decimal Cantidad { get; set; }

        public string? UnidadMedida { get; set; }
    }
    public class DevolucionVentaInputModel
    {
        public int IdVenta { get; set; }
        public string? Motivo { get; set; }
        public string? UsuarioRegistro { get; set; }
    }
}