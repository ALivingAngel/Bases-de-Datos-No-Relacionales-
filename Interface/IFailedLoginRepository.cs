using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiDatas.DTO;
using WebModel.Metrics;

namespace WebApiDatas.Interface
{
    public interface IFailedLoginRepository
    {
        Task IncrementFailedAttemptAsync(FailedLoginDto dto);
        Task<FailedLoginMetric> GetFailedAttemptsAsync(string userId, string ipAddress);
        Task ResetFailedAttemptsAsync(string userId, string ipAddress); // Opcional: Para resetear después de login exitoso
    
    }
}
