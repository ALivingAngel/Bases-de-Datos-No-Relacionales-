using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModel.Metrics;

namespace WebApiDatas.Interface
{
    public interface IResponseTimeLogRepository
    {
        Task AddAsync(ResponseTimeLog log);
    }
}
