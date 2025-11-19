using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiData.DTO
{
    public class UsuarioResponseDto
    {
        public bool Result { get; set; }
        public string Token { get; set; }
        public List<string> Errors { get; set; }
    }
}
