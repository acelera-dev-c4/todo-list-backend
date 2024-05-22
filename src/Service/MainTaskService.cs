using Domain.Models;
using Infra.Repositories;

namespace Service;


public interface IMainTaskService
{
    MainTask Create(MainTask task);
    void Delete(int taskId);
    List<MainTask>? Get(int userId);
    MainTask Update(MainTask task, int mainTaskId);
}

public class MainTaskService : IMainTaskService
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
