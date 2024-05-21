using Domain.Models;
using Infra.Repositories;

namespace Service;

public class SubTarefaService
{
    private readonly ISubTarefaRepository _subTarefaRepository;

    public SubTarefaService(ISubTarefaRepository subTarefaRepository)
    {
        _subTarefaRepository = subTarefaRepository;
    }

    public SubTarefa Create(SubTarefa subtarefa)
    {
        return _subTarefaRepository.Create(subtarefa);
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
