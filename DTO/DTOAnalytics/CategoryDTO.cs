using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiDatas.DTO.DTOAnalytics
{
    public class CategoryDTO
    {
        public int CategoryPK { get; set; }
        public string CategoryName { get; set; }
        public DateTime? ETLLoad { get; set; }
        public int? ETLIdExecution { get; set; }
    }
}
