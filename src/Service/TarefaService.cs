using AceleraDevTodoListApi.Infra.Repositories;
using Domain.Entitys;

namespace AceleraDevTodoListApi.Services;

public class TarefaService
{
    private readonly ITarefaRepository _tarefaRepository;

    public TarefaService(ITarefaRepository tarefaRepository)
    {
        _tarefaRepository = tarefaRepository;
    }

    public Tarefa Create(Tarefa tarefa)
    {
        return _tarefaRepository.Create(tarefa);
    }

    public void Delete(int idTarefa)
    {
        _tarefaRepository.Delete(idTarefa);
    }

    public List<Tarefa>? Get(int idUsuario)
    {
        return _tarefaRepository.Get(idUsuario);
    }

    public Tarefa Update(Tarefa tarefa, int id)
    {
        return _tarefaRepository.Update(tarefa, id);
    }
}
