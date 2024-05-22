using Domain.Models;
using Infra.Repositories;

namespace Service;

public interface ITarefaService
{
    Tarefa Create(Tarefa tarefa);
    Tarefa? Find(int idTarefa);
    List<Tarefa>? Get(int idUsuario);
    Tarefa Update(Tarefa tarefa, int id);
    void Delete(int idTarefa);
}

public class TarefaService : ITarefaService
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

    public Tarefa? Find(int idTarefa)
    {
        return _tarefaRepository.Find(idTarefa);
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
