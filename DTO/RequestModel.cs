using DistribuidoraWalter.Data.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class CompraRequestModel
    {
        public int IdProveedor { get; set; }
        public DateTime Fecha { get; set; }
        public string UsuarioRegistro { get; set; }
        public List<DetalleCompraDTO> Detalles { get; set; }
    }


    public class DetalleCompraModel
    {
        public int IdProducto { get; set; }
        public decimal Cantidad { get; set; }
        public int IdUnidadMedida { get; set; }
        public decimal PrecioUnitario { get; set; }
        public string CodigoLote { get; set; }
        public DateTime? FechaFabricacion { get; set; }
        public DateTime? FechaCaducidad { get; set; }
        public int IdBodega { get; set; }
    }
}
