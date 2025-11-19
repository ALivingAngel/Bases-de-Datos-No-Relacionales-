using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using WebApiDatas.DTO;
using WebApiDatas.Interface;
using WebModel.Metrics;
using WebApiDatas.Settings;

namespace WebApiDatas.Repository
{
    public class FailedLoginRepository : IFailedLoginRepository
    {
        private readonly IMongoCollection<FailedLoginMetric> _collection;
        // Solo este constructor: toma IMongoDatabase
        public FailedLoginRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<FailedLoginMetric>("FailedLoginMetrics");
        }
        public async Task IncrementFailedAttemptAsync(FailedLoginDto dto)
        {
            var filter = Builders<FailedLoginMetric>.Filter.And(
                Builders<FailedLoginMetric>.Filter.Eq(m => m.UserId, dto.UserId),
                Builders<FailedLoginMetric>.Filter.Eq(m => m.IpAddress, dto.IpAddress)
            );
            var update = Builders<FailedLoginMetric>.Update
                .Inc(m => m.FailedAttempts, 1)
                .Set(m => m.LastAttempt, dto.AttemptTime)
                .SetOnInsert(m => m.FirstAttempt, dto.AttemptTime)
                .SetOnInsert(m => m.IsBlocked, false);
            await _collection.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true });
        }

        public async Task<FailedLoginMetric> GetFailedAttemptsAsync(string userId, string ipAddress)
        {
            var filter = Builders<FailedLoginMetric>.Filter.And(
                Builders<FailedLoginMetric>.Filter.Eq(m => m.UserId, userId),
                Builders<FailedLoginMetric>.Filter.Eq(m => m.IpAddress, ipAddress)
            );
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task ResetFailedAttemptsAsync(string userId, string ipAddress)
        {
            var filter = Builders<FailedLoginMetric>.Filter.And(
                Builders<FailedLoginMetric>.Filter.Eq(m => m.UserId, userId),
                Builders<FailedLoginMetric>.Filter.Eq(m => m.IpAddress, ipAddress)
            );
            var update = Builders<FailedLoginMetric>.Update
                .Set(m => m.FailedAttempts, 0)
                .Set(m => m.IsBlocked, false);
            await _collection.UpdateOneAsync(filter, update);
        }
    }
}
