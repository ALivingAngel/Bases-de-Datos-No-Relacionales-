using DistribuidoraWalter.Data.Repositories;
using DistribuidoraWalter.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DistribuidoraWalter.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentaController : ControllerBase
    {
        private readonly VentaRepository _ventaRepository;

        public VentaController()
        {
            _ventaRepository = new VentaRepository();
        }

        // GET: api/venta
        [HttpGet]
        public async Task<ActionResult<List<VentaDTO>>> ObtenerVentas()
        {
            try
            {
                var ventas = await _ventaRepository.ObtenerVentasConDetalleAsync();
                return Ok(ventas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // GET: api/venta/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VentaDTO>> ObtenerVentaPorId(int id)
        {
            try
            {
                var venta = await _ventaRepository.GetVentaByIdAsync(id);
                if (venta == null)
                    return NotFound();

                return Ok(venta);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    
[HttpPost("Registrar")]
        public async Task<IActionResult> RegistrarVenta([FromBody] VentaCreateDTO ventaDto)
        {
            if (ventaDto == null || ventaDto.Detalles == null || ventaDto.Detalles.Count == 0)
            {
                return BadRequest("Debe enviar datos válidos y al menos un detalle.");
            }

            try
            {
                int idVenta = await _ventaRepository.RegistrarVentaAsync(
                    ventaDto.IdCliente,
                    ventaDto.Fecha,
                    ventaDto.UsuarioRegistro,
                    ventaDto.Detalles
                );

                return Ok(new { IdVenta = idVenta, Mensaje = "Venta registrada correctamente." });
            }
            catch (Exception ex)
            {
                // Puedes agregar logging aquí si quieres
                return StatusCode(500, $"Error al registrar la venta: {ex.Message}");
            }
        }
    }

    // DTO para crear la venta
    public class VentaCreateDTO
    {
        public int IdCliente { get; set; }
        public DateTime Fecha { get; set; }
        public string UsuarioRegistro { get; set; } = string.Empty;
        public List<DetalleVentaDTO> Detalles { get; set; } = new List<DetalleVentaDTO>();
    }
}