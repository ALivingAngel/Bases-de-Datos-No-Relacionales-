using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DistribuidoraWalter.Data.Repositories;
using WebApiDatas.Repository;
using DistribuidoraWalter.Model;

namespace DistribuidoraWalter.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DevolucionCompraController : ControllerBase
    {
        private readonly DevolucionCompraRepository _repository;

        public DevolucionCompraController()
        {
            _repository = new DevolucionCompraRepository();
        }
        // GET api/DevolucionCompra
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var devoluciones = await _repository.GetAllDevolucionesCompraAsync();
            return Ok(devoluciones);
        }

        // GET api/DevolucionCompra/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var devolucion = await _repository.GetDevolucionCompraAsync(id);
            if (devolucion == null)
                return NotFound($"No se encontró la devolución con Id {id}");

            return Ok(devolucion);
        }
        [HttpPost("registrar")]
        public async Task<IActionResult> RegistrarDevolucion([FromBody] DevolucionCompraInputModel input)
        {
            if (input == null || input.IdCompra <= 0)
                return BadRequest("Datos de entrada inválidos.");

            try
            {
                int idDevolucion = await _repository.RegistrarDevolucionCompraAsync(input);
                return Ok(new { IdDevolucionCompra = idDevolucion });
            }
            catch (System.Exception ex)
            {
                // Aquí puedes agregar un logger si quieres
                return StatusCode(500, $"Error al registrar la devolución: {ex.Message}");
            }
        }
    }
}
