using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModel.Metrics;

namespace WebApiDatas.Interface
{
    public interface IMemoryMetricRepository
    {
        Task AddAsync(MemoryMetric metric);
        Task<IEnumerable<MemoryMetric>> GetRecentMetricsAsync(int limit = 100);

        Task<List<MemoryMetric>> GetAllAsync();

    }
}
