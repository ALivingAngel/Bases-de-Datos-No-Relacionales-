using DistribuidoraWalter.Data.Repository;
using DistribuidoraWalter.Model;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class MarcaController : ControllerBase
{
    private readonly MarcaRepository _repository = new MarcaRepository();

    [HttpGet]
    public ActionResult<List<Marca>> GetAllMarcas()
    {
        return _repository.GetAllMarcas();
    }

    [HttpGet("{id}")]
    public ActionResult<Marca> GetMarcaById(int id)
    {
        var marca = _repository.GetMarcaById(id);
        if (marca == null) return NotFound();
        return marca;
    }

    [HttpPost]
    public ActionResult<bool> InsertNewMarca([FromBody] Marca marca)
    {
        var result = _repository.InsertNewMarca(marca);
        if (!result) return BadRequest();
        return Ok(result);
    }

    [HttpPut("{id}")]
    public ActionResult<bool> UpdateMarca(int id, [FromBody] Marca marca)
    {
        if (id != marca.IdMarca) return BadRequest("ID no coincide");
        var result = _repository.UpdateMarca(marca);
        if (!result) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public ActionResult<bool> DeleteMarcaById(int id)
    {
        var result = _repository.DeleteMarcaById(id);
        if (!result) return NotFound();
        return Ok(result);
    }
}
