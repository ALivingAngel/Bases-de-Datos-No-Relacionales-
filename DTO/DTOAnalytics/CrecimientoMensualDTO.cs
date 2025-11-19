using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiDatas.DTO.DTOAnalytics
{
    public class CrecimientoMensualDTO
    {
        public int? Anio { get; set; }
        public int? Mes { get; set; }
        public decimal? VentasTotales { get; set; }
        public decimal? PrevMonth { get; set; }
        public decimal? CrecimientoPct { get; set; }
    }
}
