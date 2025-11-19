using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiDatas.DTO.DTOAnalytics
{
    public class UsersDTO
    {
        public string UserPK { get; set; }
        public string UserName { get; set; }
        public DateTime? ETLLoad { get; set; }
        public int? ETLIdExecution { get; set; }
    }
}
