using Domain.Mappers;
using Domain.Request;
using Microsoft.AspNetCore.Mvc;
using Service;

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

    [HttpGet("{idUsuario}")]
    public IActionResult Get([FromRoute]int idUsuario)
    {
        var tarefas = _tarefaService.Get(idUsuario);
        return tarefas is null ? NotFound() : Ok(tarefas);
    }

    [HttpPost]
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

        if (tarefa.Id is not null)
            _tarefaService.Update(tarefa, (int)tarefa.Id);

        return Ok(tarefa);
    }

    [HttpDelete("{idTarefa}")]
    public IActionResult Delete(int idTarefa)
    {
        var tarefa = _tarefaService.Find(idTarefa);
        if (tarefa is null)
            return NotFound($"Tarefa não encontrada.");

        _tarefaService.Delete(idTarefa);

        return NoContent();
    }
}
