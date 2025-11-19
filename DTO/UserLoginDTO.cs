using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiData.DTO
{
    public class UsuarioLoginDTO
    {
        [Required(ErrorMessage = "El nombre de usuario es requerido.")]
        public string Nombre { get; set; } // Nombre del usuario

        [Required(ErrorMessage = "La contraseña es requerida.")]
        public string Contraseña { get; set; } // Hash de la contraseña
    }
}
