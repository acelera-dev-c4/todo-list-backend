using Domain.Models;
using Infra.Repositories;

namespace Service;

public interface IMainTaskService
{
    MainTask Create(MainTask mainTask);
    MainTask? Find(int mainTaskId);
    List<MainTask>? Get(int userId);
    MainTask Update(MainTask mainTask, int id);
    void Delete(int mainTaskId);
}

public class MainTaskService : IMainTaskService
{
    private readonly IMainTaskRepository _mainTaskRepository;

    public MainTaskService(IMainTaskRepository mainTaskRepository)
    {
        _mainTaskRepository = mainTaskRepository;
    }

    public MainTask Create(MainTask newMainTask)
    {
        return _mainTaskRepository.Create(newMainTask);
    }

    public MainTask? Find(int mainTaskId)
    {
        return _mainTaskRepository.Find(mainTaskId);
    }

    public void Delete(int mainTaskId)
    {
        _mainTaskRepository.Delete(mainTaskId);
    }

    public List<MainTask>? Get(int userId)
    {
        return _mainTaskRepository.Get(userId);
    }

    public MainTask Update(MainTask mainTask, int mainTaskId)
    {
        return _mainTaskRepository.Update(mainTask, mainTaskId);
    }
}
