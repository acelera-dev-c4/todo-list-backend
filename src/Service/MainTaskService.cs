using Domain.Models;
using Infra.Repositories;

namespace Service;

public class MainTaskService
{
    private readonly IMainTaskRepository _mainTaskRepository;

    public MainTaskService(IMainTaskRepository mainTaskRepository)
    {
        _mainTaskRepository = mainTaskRepository;
    }

    public MainTask Create(MainTask task)
    {
        return _mainTaskRepository.Create(task);
    }

    public void Delete(int taskId)
    {
        _mainTaskRepository.Delete(taskId);
    }

    public List<MainTask>? Get(int userId)
    {
        return _mainTaskRepository.Get(userId);
    }

    public MainTask Update(MainTask task, int mainTaskId)
    {
        return _mainTaskRepository.Update(task, mainTaskId);
    }
}
