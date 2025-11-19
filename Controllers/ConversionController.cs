    using Microsoft.AspNetCore.Mvc;
    using WebApiDatas.Repository;
using DistribuidoraWalter.Model;

namespace WebApi.Controllers
{
        [ApiController]
        [Route("api/[controller]")]
        public class ConversionController : ControllerBase
        {
            private readonly ConversionRepository _conversionRepository;

            public ConversionController()
            {
                _conversionRepository = new ConversionRepository();
            }

            [HttpGet]
            public ActionResult<List<Conversion>> ObtenerConversiones()
            {
                var conversiones = _conversionRepository.ObtenerConversiones();

                if (conversiones == null)
                    return StatusCode(500, "Error al obtener las conversiones");

                return Ok(conversiones);
            }
        }
    }
