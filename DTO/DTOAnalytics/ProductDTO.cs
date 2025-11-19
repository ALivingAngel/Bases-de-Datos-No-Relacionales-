using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiDatas.DTO.DTOAnalytics
{
    public class ProductDTO
    {
        public int ProductPK { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int? CategoryCod { get; set; }
        public int? BrandCod { get; set; }
        public int? MeasureCod { get; set; }
        public DateTime? ETLLoad { get; set; }
        public int? ETLIdExecution { get; set; }
    }
}
