using Microsoft.AspNetCore.Mvc;
using WebApiData.Repository;
using WebModel.DataWarehouse;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryDwController : ControllerBase
    {
        private readonly CategoryDwRepository Repository = new CategoryDwRepository();

        [HttpGet]
        public IActionResult GetAllCategoryDw()
        {
            var lista = Repository.GetAllCategoryDw() ?? new List<CategoryDw>();
            return Ok(lista);
        }
    }
}
