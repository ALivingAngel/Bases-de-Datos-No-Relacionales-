using DistribuidoraWalter.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApiDatas.Repository;

namespace DistribuidoraWalter.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoteController : ControllerBase
    {
        private readonly LoteRepository _repository;

        public LoteController()
        {
            _repository = new LoteRepository();
        }

        // GET: api/Lote
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lotes = await _repository.GetAllLotesAsync();
            return Ok(lotes);
        }
    }
}
