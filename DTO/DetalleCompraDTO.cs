using DistribuidoraWalter.Data.Repositories;
using WebApi.Models;

namespace DistribuidoraWalter.Data.Repositories
{
    // DTO para detalle compra
    public class DetalleCompraDTO
    {
        public int IdProducto { get; set; }
        public decimal Cantidad { get; set; }
        public int IdUnidadMedida { get; set; } // Aquí la unidad con la que ingresas la compra
        public decimal PrecioUnitario { get; set; }
        public string? CodigoLote { get; set; }
        public DateTime? FechaFabricacion { get; set; }
        public DateTime? FechaCaducidad { get; set; }
        public int IdBodega { get; set; }
    }
}

public class DetalleCompraDTOGet
{
    public int IdDetalleCompra { get; set; }
    public int IdProducto { get; set; }
    public string NombreProducto { get; set; }
    public int IdBodega { get; set; }
    public int IdLote { get; set; }
    public decimal Cantidad { get; set; }
    public int IdUnidadMedida { get; set; }
    public string AbreviaturaUnidad { get; set; }
    public decimal PrecioUnitario { get; set; }
}

public class CompraDTO
{
    public int IdCompra { get; set; }
    public int IdProveedor { get; set; }
    public DateTime Fecha { get; set; }
    public decimal Total { get; set; }
    public string UsuarioRegistro { get; set; }
    public List<DetalleCompraDTOGet> Detalles { get; set; } = new List<DetalleCompraDTOGet>();
}

