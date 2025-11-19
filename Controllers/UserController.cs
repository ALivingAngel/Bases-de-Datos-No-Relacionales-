using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiData.DTO;
using WebApiData.Repository;
using WebApiDatas.DTO;
using WebApiDatas.Interface;
using WebModel;

namespace Web_Walter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioRepository _usuarioRepository;
        private readonly IFailedLoginRepository _failedLoginRepo;
        public UsuarioController(UsuarioRepository usuarioRepository, IFailedLoginRepository failedLoginRepo)
        {
            _usuarioRepository = usuarioRepository;
            _failedLoginRepo = failedLoginRepo;
        }
      
            // Endpoint original - usuarios simples
            [HttpGet]
            public ActionResult<List<UsuarioDto>> GetAllUsuarios()
            {
                var usuarios = _usuarioRepository.GetAllUsuario(); // Método que ya tienes

                if (usuarios == null)
                    return BadRequest("Error al obtener los usuarios.");

                return Ok(usuarios);
            }

            // Nuevo endpoint - usuarios con lista de roles como strings
            [HttpGet("con-roles")]
            public ActionResult<List<UsuarioDto>> GetAllUsuariosConRoles()
            {
                var usuariosConRoles = _usuarioRepository.GetAllUsersWithRoles(); // Método que devuelve usuarios con Roles como List<string>

                if (usuariosConRoles == null)
                    return BadRequest("Error al obtener los usuarios con roles.");

                return Ok(usuariosConRoles);
            }
        
    

    //[Authorize(Roles = "Gerente")]
    [HttpPost]
        public UsuarioResponseDto InserNewUsuario(UsuarioRegisterDTO usuario)
        {
            var result = _usuarioRepository.InsertNewUsuario(usuario);

            return result;
        }
        // DELETE: api/Usuario/5
        [HttpDelete("{id}")]
        public IActionResult DeleteUsuario(int id)
        {
            bool eliminado = _usuarioRepository.DeleteUsuarioById(id);

            if (eliminado)
                return Ok(new { mensaje = "Usuario eliminado correctamente." });

            return NotFound(new { mensaje = "Usuario no encontrado." });
        }
        [HttpPut("{id}")]
        public IActionResult UpdateUsuario(int id, [FromBody] UsuarioUpdateDto dto)
        {
            var usuario = new Usuario
            {
                IdUsuario = id,               // Se toma de la URL
                Nombre = dto.Nombre,
                Contraseña = dto.Contraseña
            };

            bool actualizado = _usuarioRepository.UpdateUsuario(usuario);

            if (actualizado)
                return Ok(new { mensaje = "Usuario actualizado correctamente." });

            return NotFound(new { mensaje = "Usuario no encontrado." });
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> AuthenticateUsuario([FromBody] UsuarioLoginDTO usuario)
        {
            var result = _usuarioRepository.Authenticate(usuario);
            // Verifica si la autenticación fue exitosa
            // Asume que result.Token != null indica éxito; ajusta según tu UsuarioResponseDto
            if (result == null || string.IsNullOrEmpty(result.Token)) // O result.IsSuccess == false, etc.
            {
                // Autenticación fallida: Registrar intento fallido
                var dto = new FailedLoginDto
                {
                    UserId = usuario.Nombre, // O el campo que uses para identificar al usuario
                    IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    AttemptTime = DateTime.UtcNow
                };
                await _failedLoginRepo.IncrementFailedAttemptAsync(dto);
                // Opcional: Verificar si debe bloquearse (ej. después de 5 intentos)
                var metric = await _failedLoginRepo.GetFailedAttemptsAsync(dto.UserId, dto.IpAddress);
                if (metric != null && metric.FailedAttempts >= 5)
                {
                    return StatusCode(429, "Demasiados intentos fallidos. Intente más tarde."); // 429 Too Many Requests
                }
                // Retorna error de autenticación
                return Unauthorized("Credenciales inválidas.");
            }
            // Autenticación exitosa: Resetear contador de fallos
            await _failedLoginRepo.ResetFailedAttemptsAsync(usuario.Nombre, HttpContext.Connection.RemoteIpAddress?.ToString());
            // Retorna el resultado exitoso
            return Ok(result);
        }
        //public UsuarioResponseDto AuthenticateUsuario(UsuarioLoginDTO usuario)
        //{
        //    var result = _usuarioRepository.Authenticate(usuario);

        //    return result;
        //}
    }
}
