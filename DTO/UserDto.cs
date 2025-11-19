using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiDatas.DTO
{
    public class UsuarioDto
    {
       
            public int IdUsuario { get; set; }
            public string Nombre { get; set; }
            public string Contraseña { get; set; }
            public DateTime FechaRegistro { get; set; }

            // Lista de roles que tiene el usuario
            public List<string> Roles { get; set; }

    }
}
