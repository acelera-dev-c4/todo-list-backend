using Infra.DB;
using Domain.Mappers;
using Domain.Request;
using Microsoft.AspNetCore.Mvc;
using AceleraDevTodoListApi.Services;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TarefaController : ControllerBase
{
    private readonly TarefaService _tarefaService;

    public TarefaController(TarefaService tarefaService)
    {
        _tarefaService = tarefaService;
    }   

    [HttpGet("Lista")]
    public IActionResult Get()
    {
        return Ok(_tarefaService.);
    }

    [HttpGet("{idUsuario}")]
    public IActionResult Get([FromRoute]int idUsuario)
    {
        var tarefas = _tarefaService.Get(idUsuario);
        return tarefas is null ? NotFound() : Ok(tarefas);
    }

    [HttpPost()]
    public IActionResult Post([FromBody] RequisicaoTarefa requisicaoTarefa)
    {
        var novaTarefa = MapeadorTarefa.ParaClasse(requisicaoTarefa);
        _tarefaService.Create(novaTarefa);
        return Created();
    }

    [HttpPut("{idTarefa}")]
    public IActionResult Put(int idTarefa, [FromBody] TarefaUpdateRequest updateDescription)
    {
        var tarefa = _tarefaService.Find(idTarefa);

        if (tarefa is null)
            return NotFound($"Tarefa não encontrada.");

        tarefa.Descricao = updateDescription.Description;


        _tarefaService.Update(tarefa, (int)tarefa.Id);

        return Ok(tarefa);
    }

    [HttpDelete("Delete")]
    public IActionResult Delete(int Id)
    {
        var tarefa = _tarefaService.Find(Id);
        if (tarefa is null)
            return NotFound($"Tarefa não encontrada.");

        _tarefaService.Delete(Id);

        return NoContent();
    }
}
