using DistribuidoraWalter.Data.Repository;
using DistribuidoraWalter.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DistribuidoraWalter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriaRepository _repository = new CategoriaRepository();

        // GET: api/categoria
        [HttpGet]
        public ActionResult<List<Categoria>> GetAll()
        {
            var categorias = _repository.GetAllCategoria();
            return Ok(categorias);
        }

        // GET: api/categoria/5
        [HttpGet("{id}")]
        public ActionResult<Categoria> GetById(int id)
        {
            var categoria = _repository.GetCategoriaById(id);
            if (categoria == null) return NotFound();
            return Ok(categoria);
        }

        // POST: api/categoria
        [HttpPost]
        public ActionResult Create(Categoria categoria)
        {
            var result = _repository.InsertNewCategoria(categoria);
            if (!result) return BadRequest();
            return CreatedAtAction(nameof(GetById), new { id = categoria.IdCategoria }, categoria);
        }

        // PUT: api/categoria/5
        [HttpPut("{id}")]
        public ActionResult Update(int id, Categoria categoria)
        {
            if (id != categoria.IdCategoria) return BadRequest();

            var result = _repository.UpdateCategoria(categoria);
            if (!result) return NotFound();

            return NoContent();
        }

        // DELETE: api/categoria/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            var result = _repository.DeleteCategoriaById(id);
            if (!result) return NotFound();

            return NoContent();
        }
    }
}
