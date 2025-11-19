using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiData.Repository;
using System.Collections.Generic;
using DistribuidoraWalter.Model;
// using Microsoft.AspNetCore.Authorization;

namespace Web_Walter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnidadMedidaController : ControllerBase
    {
        // Instancia del repositorio donde se encuentran los métodos de acceso
        UnidadMedidaRepository Repository = new UnidadMedidaRepository();

        // [Authorize(Roles = "Gerente,Vendedor")]
        // Obtener todas las unidades de medida
        [HttpGet]
        public IActionResult GetAllUnidadMedida()
        {
            try
            {
                var list = Repository.GetAllUnidadMedida();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        // [Authorize(Roles = "Gerente,Vendedor")]
        // Obtener una unidad de medida por su ID
        [HttpGet("{id}")]
        public UnidadMedida GetUnidadMedidaById(int id)
        {
            var unidad = Repository.GetUnidadMedidaById(id);
            return unidad;
        }

        // [Authorize(Policy = "Gerente")]
        // Insertar una nueva unidad de medida
        [HttpPost]
        public bool InsertUnidadMedida(UnidadMedida unidad)
        {
            var result = Repository.InsertUnidadMedida(unidad);
            return result;
        }

        // [Authorize(Policy = "Gerente")]
        // Actualizar una unidad de medida existente
        [HttpPut("{id}")]
        public bool UpdateUnidadMedida(UnidadMedida unidad)
        {
            var result = Repository.UpdateUnidadMedida(unidad);
            return result;
        }

        // [Authorize(Policy = "Gerente")]
        // Eliminar una unidad de medida por su ID
        [HttpDelete("{id}")]
        public bool DeleteUnidadMedidaById(int id)
        {
            return Repository.DeleteUnidadMedidaById(id);
        }
    }
}
