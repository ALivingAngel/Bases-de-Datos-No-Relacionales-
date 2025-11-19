using System.Collections.Generic;
using System.Linq;
using WebApi.Models; // Modelos usados en la API
using DistribuidoraWalter.Data.Repositories; // DTOs usados en el repositorio

public static class MapperCompra
{
    public static List<DetalleCompraDTO> MapToDetalleCompraDTO(List<DetalleCompraModel> detalles)
    {
        return detalles.Select(d => new DetalleCompraDTO
        {
            IdProducto = d.IdProducto,
            Cantidad = d.Cantidad,
            IdUnidadMedida = d.IdUnidadMedida,
            PrecioUnitario = d.PrecioUnitario,
            CodigoLote = d.CodigoLote,
            FechaFabricacion = d.FechaFabricacion,
            FechaCaducidad = d.FechaCaducidad,
            IdBodega = d.IdBodega
        }).ToList();
    }
}
