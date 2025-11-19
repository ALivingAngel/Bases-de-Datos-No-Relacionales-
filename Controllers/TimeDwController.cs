using Microsoft.AspNetCore.Mvc;
using WebApiData.Repository;
using WebModel.DataWarehouse;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeDwController : ControllerBase
    {
        private readonly TimeDwRepository Repository = new TimeDwRepository();

        // GET api/TimeDw
        [HttpGet]
        public IActionResult GetAllTimeDw()
        {
            var lista = Repository.GetAllTimeDw() ?? new List<DIM_Time>();
            return Ok(lista);
        }

        // GET api/TimeDw/CrecimientoMensual
        [HttpGet("CrecimientoMensual")]
        public IActionResult GetCrecimientoMensualDw()
        {
            var lista = Repository.GetCrecimientoMensualDw() ?? new List<CrecimientoMensual>();
            return Ok(lista);
        }
    }
}
