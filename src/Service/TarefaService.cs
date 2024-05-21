using Domain.Models;
using Infra.Repositories;

namespace Service;

public class TarefaService
{
    private readonly IMainTaskRepository _tarefaRepository;

    public TarefaService(IMainTaskRepository tarefaRepository)
    {
        _tarefaRepository = tarefaRepository;
    }

    public MainTask Create(MainTask tarefa)
    {
        return _tarefaRepository.Create(tarefa);
    }

    public void Delete(int idTarefa)
    {
        _tarefaRepository.Delete(idTarefa);
    }

    public List<MainTask>? Get(int idUsuario)
    {
        return _tarefaRepository.Get(idUsuario);
    }

    public MainTask Update(MainTask tarefa, int id)
    {
        return _tarefaRepository.Update(tarefa, id);
    }
}
