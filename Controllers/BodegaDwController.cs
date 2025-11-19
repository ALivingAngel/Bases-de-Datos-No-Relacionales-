using Microsoft.AspNetCore.Mvc;
using WebApiData.Repository;
using WebModel.DataWarehouse;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BodegaDwController : ControllerBase
    {
        private readonly BodegaDwRepository Repository = new BodegaDwRepository();

        [HttpGet]
        public IActionResult GetAllBodegasDw()
        {
            var lista = Repository.GetAllBodegasDw() ?? new List<DimBodega>();
            return Ok(lista);
        }
    }
}
