using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiDatas.DTO.DTOAnalytics
{
    public class CostoPromedioProductoDTO
    {
        public string ProductName { get; set; }
        public int? TotalUnidades { get; set; }
        public decimal? GastoTotal { get; set; }
        public decimal? CostoPromedioUnitario { get; set; }
    }
}
