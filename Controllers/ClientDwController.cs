using Microsoft.AspNetCore.Mvc;
using WebApiData.Repository;
using WebModel.DataWarehouse;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientDwController : ControllerBase
    {
        private readonly ClientDwRepository Repository = new ClientDwRepository();

        [HttpGet]
        public IActionResult GetAllClientDw()
        {
            var lista = Repository.GetAllClientDw() ?? new List<DIM_ClientDw>();
            return Ok(lista);
        }
    }
}
