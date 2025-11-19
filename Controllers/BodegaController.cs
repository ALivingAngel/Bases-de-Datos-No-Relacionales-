using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WebApiData.Repository;
using System.Collections.Generic;
using DistribuidoraWalter.Model;
// using Microsoft.AspNetCore.Authorization;

namespace Web_Walter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BodegaController : ControllerBase
    {
        private readonly BodegaRepository Repository = new BodegaRepository();

        // [Authorize(Roles = "Gerente,Vendedor")]
        [HttpGet]
        public List<Bodega> GetAllBodega()
        {
            return Repository.GetAllBodega();
        }

        // [Authorize(Roles = "Gerente,Vendedor")]
        [HttpGet("{id}")]
        public Bodega GetBodegaById(int id)
        {
            return Repository.GetBodegaById(id);
        }

        // [Authorize(Policy = "Gerente")]
        [HttpPost]
        public bool InsertBodega(Bodega bodega)
        {
            return Repository.InsertBodega(bodega);
        }

        // [Authorize(Policy = "Gerente")]
        [HttpPut("{id}")]
        public bool UpdateBodega(Bodega bodega)
        {
            return Repository.UpdateBodega(bodega);
        }

        // [Authorize(Policy = "Gerente")]
        [HttpDelete("{id}")]
        public bool DeleteBodegaById(int id)
        {
            return Repository.DeleteBodegaById(id);
        }
    }
}
