using DistribuidoraWalter.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DistribuidoraWalter.Data.Repositories
{
    public class CompraRepository
    {
        public CompraRepository()
        {
            // Constructor vacío
        }
        public async Task<CompraModel?> GetCompraByIdAsync(int idCompra)
        {
            var connectionString = DBConnection.Connect();

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand("Transaccion.ObtenerCompraPorId", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@IdCompra", idCompra);

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();

            CompraModel? compra = null;

            // Leer cabecera
            if (await reader.ReadAsync())
            {
                compra = new CompraModel
                {
                    IdCompra = reader.GetInt32(reader.GetOrdinal("IdCompra")),
                    IdProveedor = reader.GetInt32(reader.GetOrdinal("IdProveedor")),
                    Fecha = reader.GetDateTime(reader.GetOrdinal("Fecha")),
                    Total = reader.GetDecimal(reader.GetOrdinal("Total")),
                    UsuarioRegistro = reader.GetString(reader.GetOrdinal("UsuarioRegistro")),
                    Detalles = new List<DetalleCompraModel>()
                };
            }
            else
            {
                return null; // No existe compra con ese Id
            }

            // Leer detalles (segundo resultset)
            if (await reader.NextResultAsync())
            {
                while (await reader.ReadAsync())
                {
                    var detalle = new DetalleCompraModel
                    {
                        IdProducto = reader.GetInt32(reader.GetOrdinal("IdProducto")),
                        NombreProducto = reader.GetString(reader.GetOrdinal("NombreProducto")),
                        IdBodega = reader.GetInt32(reader.GetOrdinal("IdBodega")),
                        NombreBodega = reader.GetString(reader.GetOrdinal("NombreBodega")),
                        IdLote = reader.GetInt32(reader.GetOrdinal("IdLote")),
                        CodigoLote = reader.GetString(reader.GetOrdinal("CodigoLote")),
                        Cantidad = reader.GetDecimal(reader.GetOrdinal("Cantidad")),
                        PrecioUnitario = reader.GetDecimal(reader.GetOrdinal("PrecioUnitario")),
                        IdUnidadMedida = reader.GetInt32(reader.GetOrdinal("IdUnidadMedida")),
                        AbreviaturaUnidadMedida = reader.GetString(reader.GetOrdinal("AbreviaturaUnidadMedida"))
                    };
                    compra.Detalles.Add(detalle);
                }
            }

            return compra;
        }

        public async Task<List<CompraDTO>> ObtenerComprasConDetalleAsync()
        {
            var compras = new List<CompraDTO>();

            string connectionString = DBConnection.Connect();

            await using SqlConnection conn = new SqlConnection(connectionString);
            await conn.OpenAsync();

            await using SqlCommand cmd = new SqlCommand("Transaccion.ObtenerComprasConDetalle", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            await using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            // Usamos un diccionario para agrupar detalles por compra y evitar duplicados
            var dictCompras = new Dictionary<int, CompraDTO>();

            while (await reader.ReadAsync())
            {
                int idCompra = reader.GetInt32(reader.GetOrdinal("IdCompra"));

                if (!dictCompras.TryGetValue(idCompra, out CompraDTO compra))
                {
                    compra = new CompraDTO
                    {
                        IdCompra = idCompra,
                        IdProveedor = reader.GetInt32(reader.GetOrdinal("IdProveedor")),
                        Fecha = reader.GetDateTime(reader.GetOrdinal("Fecha")),
                        Total = reader.GetDecimal(reader.GetOrdinal("Total")),
                        UsuarioRegistro = reader.IsDBNull(reader.GetOrdinal("UsuarioRegistro")) ? null : reader.GetString(reader.GetOrdinal("UsuarioRegistro")),
                    };
                    dictCompras.Add(idCompra, compra);
                }

                // Leer detalles
                if (!reader.IsDBNull(reader.GetOrdinal("IdDetalleCompra")))
                {
                    var detalle = new DetalleCompraDTOGet
                    {
                        IdDetalleCompra = reader.GetInt32(reader.GetOrdinal("IdDetalleCompra")),
                        IdProducto = reader.GetInt32(reader.GetOrdinal("IdProducto")),
                        NombreProducto = reader.IsDBNull(reader.GetOrdinal("NombreProducto")) ? null : reader.GetString(reader.GetOrdinal("NombreProducto")),
                        IdBodega = reader.GetInt32(reader.GetOrdinal("IdBodega")),
                        IdLote = reader.GetInt32(reader.GetOrdinal("IdLote")),
                        Cantidad = reader.GetDecimal(reader.GetOrdinal("Cantidad")),
                        IdUnidadMedida = reader.GetInt32(reader.GetOrdinal("IdUnidadMedida")),
                        AbreviaturaUnidad = reader.IsDBNull(reader.GetOrdinal("AbreviaturaUnidad")) ? null : reader.GetString(reader.GetOrdinal("AbreviaturaUnidad")),
                        PrecioUnitario = reader.GetDecimal(reader.GetOrdinal("PrecioUnitario")),
                    };
                    compra.Detalles.Add(detalle);
                }
            }

            compras.AddRange(dictCompras.Values);

            return compras;
        }
    
    public async Task<int> RegistrarCompraAsync(int idProveedor, DateTime fecha, string usuarioRegistro, List<DetalleCompraDTO> detalles)
        {
            if (detalles == null || detalles.Count == 0)
                throw new ArgumentNullException(nameof(detalles), "Debe especificar al menos un detalle de compra.");

            DataTable dtDetalles = ConstruirDataTableDetalles(detalles);

            try
            {
                string connectionString = DBConnection.Connect();

                await using SqlConnection conn = new SqlConnection(connectionString);
                await conn.OpenAsync();

                await using SqlCommand cmd = new SqlCommand("Transaccion.RegistrarCompra", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@IdProveedor", idProveedor);
                cmd.Parameters.AddWithValue("@Fecha", fecha);
                cmd.Parameters.AddWithValue("@UsuarioRegistro", usuarioRegistro ?? (object)DBNull.Value);

                SqlParameter detallesParam = cmd.Parameters.AddWithValue("@Detalles", dtDetalles);
                detallesParam.SqlDbType = SqlDbType.Structured;
                detallesParam.TypeName = "dbo.TipoDetalleCompra";

                SqlParameter outputIdCompra = new SqlParameter("@IdCompra", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputIdCompra);

                await cmd.ExecuteNonQueryAsync();

                return outputIdCompra.Value != DBNull.Value ? (int)outputIdCompra.Value : throw new InvalidOperationException("No se pudo obtener el Id de la compra generada.");
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Error SQL al registrar la compra.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error inesperado al registrar la compra.", ex);
            }
        }

        private static DataTable ConstruirDataTableDetalles(List<DetalleCompraDTO> detalles)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("IdProducto", typeof(int));
            dt.Columns.Add("Cantidad", typeof(decimal));
            dt.Columns.Add("IdUnidadMedida", typeof(int));    // Aquí está el IdUnidadMedida
            dt.Columns.Add("PrecioUnitario", typeof(decimal));
            dt.Columns.Add("CodigoLote", typeof(string));
            dt.Columns.Add("FechaFabricacion", typeof(DateTime));
            dt.Columns.Add("FechaCaducidad", typeof(DateTime));
            dt.Columns.Add("IdBodega", typeof(int));
            
            foreach (var d in detalles)
            {
                dt.Rows.Add(
                    d.IdProducto,
                    d.Cantidad,
                    d.IdUnidadMedida,  // Valor enviado
                    d.PrecioUnitario,
                    d.CodigoLote ?? string.Empty,
                    d.FechaFabricacion ?? (object)DBNull.Value,
                    d.FechaCaducidad ?? (object)DBNull.Value,
                    d.IdBodega
                );
            }

            return dt;
        }
    }
}
