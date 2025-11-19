using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiDatas.DTO
{
    public class UsuarioUpdateDto
    {
        public string Nombre { get; set; }
        public string Contraseña { get; set; }   // texto plano, será hasheada
    }
}
