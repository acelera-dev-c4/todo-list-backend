using Domain.Models;
using Infra.Repositories;

namespace Service;


public interface IMainTaskService
{
    MainTask Create(MainTask task);
    void Delete(int taskId);
    List<MainTask>? Get(int userId);
    MainTask? Find(int mainTaskId);
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

    public List<MainTask>? Get(int userId)
    {
        return _mainTaskRepository.Get(userId);
    }

    public MainTask? Find(int mainTaskId)
    {
        return _mainTaskRepository.Find(mainTaskId);
    }

    public MainTask Update(MainTask task, int mainTaskId)
    {
        return _mainTaskRepository.Update(task, mainTaskId);
    }
    public void Delete(int mainTaskId)
    {
        _mainTaskRepository.Delete(mainTaskId);
    }

}

