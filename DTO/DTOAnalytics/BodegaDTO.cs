using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiDatas.DTO.DTOAnalytics
{
    public class BodegaDTO
    {
        public int IdBodega { get; set; }
        public string NombreBodega { get; set; }
        public string Ubicacion { get; set; }
        public string Tipo { get; set; }
        public DateTime? ETLLoad { get; set; }
        public int? ETLIdExecution { get; set; }
    }
}
