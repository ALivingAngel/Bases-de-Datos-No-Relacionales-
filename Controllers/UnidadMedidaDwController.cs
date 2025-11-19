using Microsoft.AspNetCore.Mvc;
using WebApiData.Repository;
using WebModel.DataWarehouse;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnidadMedidaDwController : ControllerBase
    {
        private readonly UnidadMedidaDwRepository Repository = new UnidadMedidaDwRepository();

        // GET api/UnidadMedidaDw
        [HttpGet]
        public IActionResult GetAllUnidadMedidaDw()
        {
            var lista = Repository.GetAllUnidadMedidaDw() ?? new List<DimUnidadMedida>();
            return Ok(lista);
        }
    }
}
