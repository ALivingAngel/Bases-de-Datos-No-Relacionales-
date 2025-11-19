using System.ComponentModel.DataAnnotations;

namespace WebApiData.DTO
{
    public class UsuarioRegisterDTO
    {
        [Required(ErrorMessage = "El nombre de usuario es requerido.")]
        public string Nombre { get; set; } // Nombre del usuario

        [Required(ErrorMessage = "La contraseña es requerida.")]
        public string Contraseña { get; set; } // Hash de la contraseña
    }
}
