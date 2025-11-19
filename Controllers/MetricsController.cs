using Microsoft.AspNetCore.Mvc;
using WebApiDatas.Interface;

namespace WebApi.Controllers
{
    public class MetricsController : Controller
    {
        private readonly IMemoryMetricRepository _repository;

        public MetricsController(IMemoryMetricRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("memory")]
        public async Task<IActionResult> GetRecent()
        {
            var metrics = await _repository.GetRecentMetricsAsync();
            return Ok(metrics);
        }
    }
}
