using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiDatas.MongoDB
{
    public class MongoSettings
    {
        public string ConnectionString { get; set; }
        public string SecurityDatabase { get; set; }
        public string SecurityCollection { get; set; }
        public string TrustedIpCollection { get; set; }
    }
}
