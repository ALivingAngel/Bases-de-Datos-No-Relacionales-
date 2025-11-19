using Microsoft.AspNetCore.Mvc;
using WebApiData.Repository;
using WebModel.DataWarehouse;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersDwController : ControllerBase
    {
        private readonly UsersDwRepository Repository = new UsersDwRepository();

        // GET api/UsersDw
        [HttpGet]
        public IActionResult GetAllUsersDw()
        {
            var lista = Repository.GetAllUsersDw() ?? new List<DIM_Users>();
            return Ok(lista);
        }
    }
}
