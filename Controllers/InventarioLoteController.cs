using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApiDatas.Repository;

namespace DistribuidoraWalter.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventarioLoteController : ControllerBase
    {
        private readonly InventarioRepository _repository;

        public InventarioLoteController()
        {
            _repository = new InventarioRepository();
        }

        // GET: api/InventarioLote
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var inventario = await _repository.GetAllInventarioLoteAsync();
            return Ok(inventario);
        }
    }
}
