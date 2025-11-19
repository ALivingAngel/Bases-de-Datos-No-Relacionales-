namespace WebApiDatas.DTO.DTOAnalytics
{
    public class CompraDTOAnalytics
    {
        public int IdCompra { get; set; }
        public int? IdTiempo { get; set; }
        public int? IdProveedor { get; set; }
        public string? IdUsuario { get; set; }  
        public int? IdProducto { get; set; }
        public int? IdCategoria { get; set; }
        public int? IdMarca { get; set; }
        public int? IdUnidadMedida { get; set; }
        public int? IdBodega { get; set; }
        public decimal? Cantidad { get; set; }
        public decimal? PrecioUnitario { get; set; }
        public decimal? TotalCompra { get; set; }
        public DateTime? ETLLoad { get; set; }
        public int? ETLIdExecution { get; set; }
    }
}
