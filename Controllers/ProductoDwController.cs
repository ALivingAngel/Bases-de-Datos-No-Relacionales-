using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApiData.Repository;
using WebModel.DataWarehouse;
using static WebModel.DataWarehouse.CostoPromedioProducto;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDwController : ControllerBase
    {
        private readonly ProductDwRepository Repository = new ProductDwRepository();

        // GET api/ProductDw
        [HttpGet]
        public IActionResult GetAllProductDw()
        {
            var lista = Repository.GetAllProductDw() ?? new List<DIM_Product>();
            return Ok(lista);
        }

        // GET api/ProductDw/CostoPromedio
        [HttpGet("CostoPromedio")]
        public IActionResult GetCostoPromedioProductosDw()
        {
            var lista = Repository.GetCostoPromedioProductosDw() ?? new List<CostoPromedioProducto>();
            return Ok(lista);
        }

        // GET api/ProductDw/UnidadesVendidas
        [HttpGet("UnidadesVendidas")]
        public IActionResult GetUnidadesVendidasXProductoDw()
        {
            var lista = Repository.GetUnidadesVendidasXProductoDw() ?? new List<UnidadesVendidasProducto>();
            return Ok(lista);
        }
    }
}
