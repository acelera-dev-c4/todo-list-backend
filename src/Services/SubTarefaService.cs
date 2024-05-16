using AceleraDevTodoListApi.Infra.Repositories.SubTarefaRepository;

namespace AceleraDevTodoListApi.Services;

public class SubTarefaService
{
    private readonly ISubTarefaRepository _subTarefaRepository;
    public SubTarefaService(IUSubTarefaRepository subTarefaRepository) {
        _subTarefaRepository = subTarefaRepository;
    }

    public SubTarefa Create(SubTarefa subtarefa)
    {
        return _subTarefaRepository.Create(subtarefa);
    }

    public void Delete(int id)
    {
        _subTarefaRepository.Delete(id);
    }

    public SubTarefa? GetById(int id)
    {
        return _subTarefaRepository.GetById(id);
    }

    public List<SubTarefa> List()
    {
        var subTarefas = _subTarefaRepository.GetAll();
        return subTarefas;

    }

    public SubTarefa Update(SubTarefa subtarefaUpdate, int id)
    {
        return _subTarefaRepository.Update(subtarefaUpdate, id);
    }


}
