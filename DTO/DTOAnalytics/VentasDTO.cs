using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiDatas.DTO.DTOAnalytics
{
    public class VentasDTO
    {
        public int FactVentasPK { get; set; }
        public string UserPK { get; set; }
        public int? TimePK { get; set; }
        public int? CLientPK { get; set; }
        public int? ProductPK { get; set; }
        public int? CategoryPK { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? Total { get; set; }
        public DateTime? ETLLoad { get; set; }
        public int? ETLIdExecution { get; set; }
    }
}
