using Domain.Models;
using Infra.Repositories;

namespace Service;


public interface ISubTarefaService
{
    SubTarefa Create(SubTarefa subTarefa);
    SubTarefa? Find(int idSubTarefa);
    List<SubTarefa> List(int idTarefa);
    SubTarefa Update(SubTarefa subtarefaUpdate, int id);
    void Delete(int idSubTarefa);
}
public class SubTarefaService : ISubTarefaService
{
    private readonly ISubTarefaRepository _subTarefaRepository;

    public SubTarefaService(ISubTarefaRepository subTarefaRepository)
    {
        _subTarefaRepository = subTarefaRepository;
    }

    public SubTarefa Create(SubTarefa subTarefa)
    {
        return _subTarefaRepository.Create(subTarefa);
    }

    public SubTarefa? Find(int idSubTarefa)
    {
        return _subTarefaRepository.Find(idSubTarefa);
    }

    public void Delete(int idSubTarefa)
    {
        _subTarefaRepository.Delete(idSubTarefa);
    }

    public List<SubTarefa> List(int idTarefa)
    {
        var subTarefas = _subTarefaRepository.Get(idTarefa);
        return subTarefas;
    }

    public SubTarefa Update(SubTarefa subtarefaUpdate, int id)
    {
        return _subTarefaRepository.Update(subtarefaUpdate, id);
    }
}
