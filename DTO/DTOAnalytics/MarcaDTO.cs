using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiDatas.DTO.DTOAnalytics
{
    public class MarcaDTO
    {
        public int IdMarca { get; set; }
        public string NombreMarca { get; set; }
        public string Descripcion { get; set; }
        public DateTime? ETLLoad { get; set; }
        public int? ETLIdExecution { get; set; }
    }
}
