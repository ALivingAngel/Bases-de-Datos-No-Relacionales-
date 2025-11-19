using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApiData.DTO;
using WebApiData.Repository;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RolRepository _rolRepository;

        public RolesController(RolRepository rolRepository)
        {
            _rolRepository = rolRepository;
        }

        [HttpGet]
        public ActionResult<List<Rol>> GetAllRoles()
        {
            var roles = _rolRepository.GetAllRol();
            if (roles == null)
                return NotFound("Roles not found.");
            return Ok(roles);
        }

        public class CrearRolDto
        {
            public string Nombre { get; set; }
        }

        [HttpPost]
        public IActionResult CrearRol([FromBody] CrearRolDto dto)
        {
            return Ok(_rolRepository.CreateRole(dto.Nombre));
        }

        [HttpPut("{id}")]
        public RolResponseDto Update(UsuarioRolDTO role)
        {
            return _rolRepository.UpdateRole(role);
        }

        // ✅ ESTE ES EL CORRECTO: ELIMINA UN ROL DEL SISTEMA COMPLETAMENTE
        [HttpDelete("{id}")]
        public IActionResult DeleteRol(int id)
        {
            var result = _rolRepository.DeleteRole(id);
            return StatusCode((int)result.StatusCode, result);
        }
        // DTO para recibir la petición
        public class AsignarRolRequest
        {
            public int IdUsuario { get; set; }
            public int IdRol { get; set; }
        }

        [HttpPost("asignar-rol")]
        public IActionResult AsignarRol([FromBody] AsignarRolRequest request)
        {
            var result = _rolRepository.AssignRole(request.IdUsuario, request.IdRol);

            if (result.StatusCode == HttpStatusCode.OK)
                return Ok(result);

            if (result.StatusCode == HttpStatusCode.NotFound)
                return NotFound(result);

            if (result.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return StatusCode(500, result); // Error inesperado
        }

        // ✅ REMOVER ROL DE UN USUARIO (sin eliminar el rol)
        [HttpDelete("usuario/{IdUsuario}/rol/{IdRol}")]
        public IActionResult RemoveUserRole(int IdUsuario, int IdRol)
        {
            var result = _rolRepository.RemoveUserRole(IdUsuario, IdRol);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
