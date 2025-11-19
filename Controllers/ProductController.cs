using DistribuidoraWalter.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApiData.Repository;

namespace Web_Walter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductRepository _repository;

        public ProductController()
        {
            _repository = new ProductRepository(); // Tu repositorio ya está así estructurado
        }

        //// GET: api/Product/GetAllProduct
        //[Authorize(Roles = "Gerente,Vendedor")]
        [HttpGet]
        public ActionResult<List<Product>> GetAllProduct()
        {
            var products = _repository.GetAllProduct();
            if (products == null)
                return StatusCode(500, "Error al obtener productos.");
            return Ok(products);
        }

        //// GET: api/Product/GetProductById?id=5
        //[Authorize(Roles = "Gerente,Vendedor")]
        [HttpGet("{id}")]
        public ActionResult<Product> GetProductById(int id)
        {
            var product = _repository.GetProductById(id);
            if (product == null)
                return NotFound("Producto no encontrado.");
            return Ok(product);
        }

        //// POST: api/Product/InsertNewProduct
        //[Authorize(Policy = "Gerente")]
        [HttpPost]
        public ActionResult<bool> InsertNewProduct([FromBody] Product product)
        {
            var result = _repository.InsertNewProduct(product);
            if (!result)
                return Conflict("El producto ya existe o no se pudo insertar.");
            return Ok(true);
        }

        [HttpPut("{id}")]
        public ActionResult<bool> UpdateProductById(int id, [FromBody] Product product)
        {
            if (id != product.IdProducto)
                return BadRequest("El ID del producto no coincide con el ID de la URL.");

            var result = _repository.UpdateProductById(product);
            if (!result)
                return NotFound("No se encontró el producto para actualizar.");
            return Ok(true);
        }


        //// DELETE: api/Product/DeleteProductById?id=5
        //[Authorize(Policy = "Gerente")]
        [HttpDelete("{id}")]
        public ActionResult<bool> DeleteProductById(int id)
        {
            var result = _repository.DeleteProductById(id);
            if (!result)
                return NotFound("No se pudo eliminar el producto o no existe.");
            return Ok(true);
        }
    }
}
