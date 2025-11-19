using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiDatas.DTO.DTOAnalytics
{
    public class ClientDTO
    {
        public int CLientPK { get; set; }
        public string ClientName { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public DateTime? ETLLoad { get; set; }
        public int? ETLExecution { get; set; }
    }

}
