using Domain.Mappers;
using Domain.Models;
using Domain.Request;
using Infra.Repositories;

namespace Service;

public interface IMainTaskService
{
    MainTask Create(MainTaskRequest mainTask);
    void Delete(int mainTaskId);
    List<MainTask>? Get(int userId);
    MainTask? Find(int mainTaskId);
    MainTask Update(MainTaskUpdate mainTask, int mainTaskId);
}

public class MainTaskService : IMainTaskService
{
    private readonly IMainTaskRepository _mainTaskRepository;

    public MainTaskService(IMainTaskRepository mainTaskRepository)
    {
        _mainTaskRepository = mainTaskRepository;
    }

    public MainTask Create(MainTaskRequest mainTaskRequest)
    {
        var newMainTask = MainTaskMapper.ToClass(mainTaskRequest);
        return _mainTaskRepository.Create(newMainTask);
    }

    public List<MainTask>? Get(int userId)
    {
        return _mainTaskRepository.Get(userId);
    }

    public MainTask? Find(int mainTaskId)
    {
        var mainTask = _mainTaskRepository.Find(mainTaskId);

        if (mainTask is null)
            throw new Exception("mainTask not found!");

        return mainTask;
    }

    public MainTask Update(MainTaskUpdate mainTaskUpdate, int mainTaskId)
    {
        var mainTask = _mainTaskRepository.Find(mainTaskId);

        if (mainTask is null)
            throw new Exception("mainTask not found!");

        mainTask.Description = mainTaskUpdate.Description;

        return _mainTaskRepository.Update(mainTask);
    }

    public void Delete(int mainTaskId)
    {
        var mainTask = _mainTaskRepository.Find(mainTaskId);

        if (mainTask is null)
            throw new Exception("mainTask not found!");

        _mainTaskRepository.Delete(mainTaskId);
    }

}

