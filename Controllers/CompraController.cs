using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DistribuidoraWalter.Data.Repositories;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompraController : ControllerBase
    {
        private readonly CompraRepository _compraRepository;

        public CompraController()
        {
            // Instancia directa porque no usas DI ni interfaces
            _compraRepository = new CompraRepository();
        }
        [HttpGet("{idCompra:int}")]
        public async Task<IActionResult> GetById(int idCompra)
        {
            var compra = await _compraRepository.GetCompraByIdAsync(idCompra);

            if (compra == null)
                return NotFound(new { Message = $"No se encontró la compra con Id {idCompra}" });

            return Ok(compra);
        }
        [HttpGet("todas")]
        public async Task<IActionResult> ObtenerTodasCompras()
        {
            try
            {
                var compras = await _compraRepository.ObtenerComprasConDetalleAsync();
                return Ok(compras);
            }
            catch (Exception ex)
            {
                // Log aquí si quieres
                return StatusCode(500, "Error al obtener las compras: " + ex.Message);
            }
        }

            [HttpPost("registrar")]
        public async Task<IActionResult> RegistrarCompra([FromBody] CompraRequestModel request)
        {
            try
            {
                if (request == null)
                    return BadRequest("Datos de compra no pueden estar vacíos.");

                if (request.Detalles == null || request.Detalles.Count == 0)
                    return BadRequest("La compra debe tener al menos un detalle.");

                if (request.Fecha.Date > DateTime.Now.Date)
                    return BadRequest("La fecha de compra no puede ser futura.");


                int idCompra = await _compraRepository.RegistrarCompraAsync(
                    request.IdProveedor,
                    request.Fecha,
                    request.UsuarioRegistro,
                    request.Detalles
                );

                return Ok(new { IdCompra = idCompra });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                // Aquí podrías loggear el error
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                // Error inesperado
                return StatusCode(500, "Error inesperado: " + ex.Message);
            }
        }
    }
}