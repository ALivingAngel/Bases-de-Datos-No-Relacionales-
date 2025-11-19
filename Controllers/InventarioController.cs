using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApiDatas.Repository;

namespace DistribuidoraWalter.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventarioController : ControllerBase
    {
        private readonly InventarioRepository _repository;

        public InventarioController()
        {
            _repository = new InventarioRepository();
        }

        // GET: api/Inventario
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var inventario = await _repository.GetAllInventarioAsync();
            return Ok(inventario);
        }

    }
}
