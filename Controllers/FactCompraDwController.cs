using Microsoft.AspNetCore.Mvc;
using WebApiData.Repository;
using WebModel.DataWarehouse;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FactCompraDwController : ControllerBase
    {
        private readonly FactCompraDwRepository Repository = new FactCompraDwRepository();

        // GET api/FactCompraDw
        [HttpGet]
        public IActionResult GetAllFactCompraDw()
        {
            var lista = Repository.GetAllFactCompraDw() ?? new List<FactCompra>();
            return Ok(lista);
        }

        // GET api/FactCompraDw/GastoTotal
        [HttpGet("GastoTotal")]
        public IActionResult GetGastoTotalComprasDw()
        {
            var lista = Repository.GetGastoTotalComprasDw() ?? new List<GastoTotalCompraDw>();
            return Ok(lista);
        }
    }
}
