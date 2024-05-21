using Domain.Entitys;
using Microsoft.AspNetCore.Mvc;
using AceleraDevTodoListApi.Services;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SubTarefaController : Controller
{
    private readonly SubTarefaService _subTarefaService;

    public SubTarefaController(SubTarefaService subTarefaService)
    {
        _subTarefaService = subTarefaService;
    }

    [HttpGet("{idTarefa}")]
    public IActionResult Get([FromRoute] int idTarefa)
    {
        var subTarefas = _subTarefaService.List(idTarefa);
        return subTarefas is null ? NotFound() : Ok(subTarefas);
    }

    [HttpPost]
    public IActionResult Post([FromBody] SubTarefa novaSubTarefa)
    {
        _subTarefaService.Create(novaSubTarefa);
        return Ok(novaSubTarefa);
    }

    [HttpPut("{idSubTarefa}")]
    public IActionResult Put([FromRoute] int idSubTarefa, [FromBody] SubTarefa updateSubTarefa)
    {
        _subTarefaService.Update(updateSubTarefa, idSubTarefa);

        return Ok(updateSubTarefa);
    }

    [HttpDelete("{idSubTarefa}")]
    public IActionResult Delete([FromRoute] int idSubTarefa)
    {
        _subTarefaService.Delete(idSubTarefa);

        return NoContent();
    }
}
