using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiDatas.DTO.DTOAnalytics
{
    public class ProveedorDTO
    {
        public int IdProveedor { get; set; }
        public string NombreProveedor { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public DateTime? ETLLoad { get; set; }
        public int? ETLIdExecution { get; set; }
        public string Contacto { get; set; }
    }
}
