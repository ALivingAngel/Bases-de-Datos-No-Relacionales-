using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApiDatas.Repository;
using DistribuidoraWalter.Model;

namespace DistribuidoraWalter.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DevolucionVentaController : ControllerBase
    {
        private readonly DevolucionVentaRepository _repository;

        public DevolucionVentaController()
        {
            _repository = new DevolucionVentaRepository();
        }

        // GET api/DevolucionVenta
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var devoluciones = await _repository.GetAllDevolucionesVentaAsync();
            return Ok(devoluciones);
        }

        // GET api/DevolucionVenta/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var devolucion = await _repository.GetDevolucionVentaAsync(id);
            if (devolucion == null)
                return NotFound($"No se encontró la devolución de venta con Id {id}");

            return Ok(devolucion);
        }

        // POST api/DevolucionVenta/registrar
        [HttpPost("registrar")]
        public async Task<IActionResult> RegistrarDevolucion([FromBody] DevolucionVentaInputModel input)
        {
            if (input == null || input.IdVenta <= 0)
                return BadRequest("Datos de entrada inválidos.");

            try
            {
                int idDevolucion = await _repository.RegistrarDevolucionVentaAsync(input);
                return Ok(new { IdDevolucionVenta = idDevolucion });
            }
            catch (System.Exception ex)
            {
                // Aquí puedes agregar un logger si quieres
                return StatusCode(500, $"Error al registrar la devolución: {ex.Message}");
            }
        }
    }
}
