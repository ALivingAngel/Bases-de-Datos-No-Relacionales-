using Microsoft.AspNetCore.Mvc;
using WebApiData.Repository;
using WebModel.DataWarehouse;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasDwController : ControllerBase
    {
        private readonly VentasDwRepository Repository = new VentasDwRepository();

        // GET api/VentasDw
        [HttpGet]
        public IActionResult GetAllVentasDw()
        {
            var lista = Repository.GetAllVentasDw() ?? new List<FACT_Ventas>();
            return Ok(lista);
        }

        // GET api/VentasDw/Totales
        [HttpGet("Totales")]
        public IActionResult GetVentasTotalesMensualDw()
        {
            var lista = Repository.GetVentasTotalesMensualDw() ?? new List<VentasTotalesMensual>();
            return Ok(lista);
        }
    }
}
