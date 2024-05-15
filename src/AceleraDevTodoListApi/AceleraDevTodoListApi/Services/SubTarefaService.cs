namespace AceleraDevTodoListApi.Services;

public class SubTarefaService
{
    private readonly ISubtarefaRepository _subtarefaRepository;
    public SubTarefaService(IUSubtarefaRepository subtarefaRepository) {
        _subtarefaRepository = subtarefaRepository;
    }

    public SubTarefa Create(SubTarefa subtarefa)
    {
        return _subtarefaRepository.Create(subtarefa);
    }

    public void Delete(int id)
    {
        _subtarefaRepository.Delete(id);
    }

    public SubTarefa? GetById(int id)
    {
        return _subtarefaRepository.GetById(id);
    }

    public List<SubTarefa> List()
    {
        var subtarefas = _subtarefaRepository.GetAll();
        return subtarefas;

    }

    public SubTarefa Update(SubTarefa subtarefaUpdate, int id)
    {
        return _subtarefaRepository.Update(subtarefaUpdate, id);
    }


}
