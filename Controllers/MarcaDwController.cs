using Microsoft.AspNetCore.Mvc;
using WebApiData.Repository;
using WebModel.DataWarehouse;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcaDwController : ControllerBase
    {
        private readonly MarcaDwRepository Repository = new MarcaDwRepository();

        [HttpGet]
        public IActionResult GetAllMarcaDw()
        {
            var lista = Repository.GetAllMarcaDw() ?? new List<DimMarca>();
            return Ok(lista);
        }
    }
}
