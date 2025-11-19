using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiDatas.DTO
{
    public class FailedLoginDto
    {
        public string UserId { get; set; }
        public string IpAddress { get; set; }
        public DateTime AttemptTime { get; set; }
    }
}
