using Infra.DB;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SubTarefaController : Controller
{
    private readonly MyDBContext _myDBContext;

    public SubTarefaController(MyDBContext myDBContext)
    {
        _myDBContext = myDBContext;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_myDBContext.SubTarefas);
    }

    [HttpGet("{idSubtarefa}")]
    public IActionResult Get(int idSubtarefa)
    {
        var subTarefa = _myDBContext.SubTarefas.Find(idSubtarefa);
        return subTarefa is null ? NotFound() : Ok(subTarefa);
    }

    [HttpPost]
    public IActionResult Post([FromBody] SubTarefa novaSubTarefa)
    {
        _myDBContext.SubTarefas.Add(novaSubTarefa);
        _myDBContext.SaveChanges();
        return Ok(novaSubTarefa);
    }

    [HttpPut("{idSubTarefa}")]
    public IActionResult Put(int idSubTarefa, [FromBody] string updateDescription)
    {
        var subTarefa = _myDBContext.Tarefas.Find(idSubTarefa);
        if (subTarefa is null)
        {
            return NotFound($"Subtarefa não encontrada.");
        }

        subTarefa.Descricao = updateDescription;

        _myDBContext.Update(subTarefa);
        _myDBContext.SaveChanges();

        return Ok(subTarefa);
    }

    [HttpDelete("{idSubTarefa}")]
    public IActionResult Delete([FromRoute] int idSubTarefa)
    {
        var subTarefa = _myDBContext.Tarefas.Find(idSubTarefa);

        if (subTarefa is null)
        {
            return NotFound($"SubTarefa não encontrada.");
        }

        _myDBContext.Remove(subTarefa);
        _myDBContext.SaveChanges();

        return NoContent();
    }
}
