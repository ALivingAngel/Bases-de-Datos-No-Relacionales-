using DistribuidoraWalter.Data.Repository;
using DistribuidoraWalter.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DistribuidoraWalter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        private readonly ProveedorRepository Repository = new ProveedorRepository();

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Proveedor Controller está activo");
        }

        [HttpGet]
        public List<Proveedor> GetAllProveedor()
        {
            var listProveedor = Repository.GetAllProveedor();
            return listProveedor;
        }

        [HttpGet("{id}")]
        public Proveedor GetProveedorById(int id)
        {
            var Proveedor = Repository.GetProveedorById(id);
            return Proveedor;
        }

        [HttpPost]
        public bool InsertNewCProveedor(Proveedor Proveedor)
        {
            var result = Repository.InsertNewProveedor(Proveedor);
            return result;
        }

        [HttpDelete("{id}")]
        public bool DeleteProveedorById(int id)
        {
            return Repository.DeleteProveedorById(id);
        }

        [HttpPut("{id}")]
        public bool UpdateProveedor(Proveedor Proveedor)
        {
            return Repository.UpdateProveedor(Proveedor);
        }
    }
}
