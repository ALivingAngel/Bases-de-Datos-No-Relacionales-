using Microsoft.AspNetCore.Mvc;
using WebApiData.Repository;
using WebModel.DataWarehouse;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorDwController : ControllerBase
    {
        private readonly ProveedorDwRepository Repository = new ProveedorDwRepository();

        // GET api/ProveedorDw
        [HttpGet]
        public IActionResult GetAllProveedorDw()
        {
            var lista = Repository.GetAllProveedorDw() ?? new List<DimProveedor>();
            return Ok(lista);
        }

        // GET api/ProveedorDw/MayorGasto
        [HttpGet("MayorGasto")]
        public IActionResult GetProveedorMayorGastoDw()
        {
            var lista = Repository.GetProveedorMayorGastoDw() ?? new List<ProveedorMayorGasto>();
            return Ok(lista);
        }
    }
}
