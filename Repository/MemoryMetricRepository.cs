using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiDatas.Interface;
using WebModel.Metrics;
using WebApiDatas.Repository; // asegúrate del namespace correcto

namespace WebApiDatas.Repository
{
    public class MemoryMetricRepository : IMemoryMetricRepository
    {
        private readonly IMongoCollection<MemoryMetric> _metrics;

        public MemoryMetricRepository(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.connectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _metrics = database.GetCollection<MemoryMetric>("MemoryMetrics");
        }

        public async Task AddAsync(MemoryMetric metric)
        {
            await _metrics.InsertOneAsync(metric);
        }

        public async Task<IEnumerable<MemoryMetric>> GetRecentMetricsAsync(int limit = 100)
        {
            return await _metrics.Find(_ => true)
                .SortByDescending(x => x.Timestamp)
                .Limit(limit)
                .ToListAsync();
        }
        public async Task<List<MemoryMetric>> GetAllAsync()
        {
            return await _metrics.Find(_ => true).ToListAsync();
        }

    }

    public class MongoDbSettings
    {
        public string connectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
    }

}


