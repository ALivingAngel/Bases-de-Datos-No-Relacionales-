
using DistribuidoraWalter.Data.Repository;
using DistribuidoraWalter.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DistribuidoraWalter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteRepository Repository = new ClienteRepository();
    
        [HttpGet]
        public List<Cliente> GetAllCliente()
        {
            var listCliente = Repository.GetAllClientes();
            return listCliente;
        }
    
        [HttpGet("{id}")]
        public Cliente GetClienteById(int id)
        {
            var cliente = Repository.GetClienteById(id);
            return cliente;
        }
     
        [HttpPost]
        public bool InsertNewCliente(Cliente cliente)
        {
            var result = Repository.InsertNewCliente(cliente);
            return result;
        }

        [HttpDelete("{id}")]
        public bool DeleteClienteById(int id)
        {
            return Repository.DeleteClienteById(id);
        }

        [HttpPut("{id}")]
        public bool UpdateCliente(Cliente cliente)
        {
            return Repository.UpdateCliente(cliente);
        }
    }
}
