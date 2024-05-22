using Domain.Models;
using Infra.Repositories;

namespace Service;


public interface ISubTaskService
{
    SubTask Create(SubTask newSubTask);
    SubTask? Find(int subTaskId);
    List<SubTask> List(int taskId);
    SubTask Update(SubTask subtarefaUpdate, int id);
    void Delete(int subTaskId);
}
public class SubTaskService : ISubTaskService
{
    private readonly ISubTaskRepository _subTaskRepository;

    public SubTaskService(ISubTaskRepository subTaskRepository)
    {
        _subTaskRepository = subTaskRepository;
    }

    public SubTask Create(SubTask newSubTask)
    {
        return _subTaskRepository.Create(newSubTask);
    }

    public SubTask? Find(int subTaskId)
    {
        return _subTaskRepository.Find(subTaskId);
    }

    public void Delete(int subTaskId)
    {
        _subTaskRepository.Delete(subTaskId);
    }

    public List<SubTask> List(int mainTaskId)
    {
        var subTasks = _subTaskRepository.Get(mainTaskId);
        return subTasks;
    }

    public SubTask Update(SubTask updatedSubTask, int subTaskId)
    {
        return _subTaskRepository.Update(updatedSubTask, subTaskId);
    }
}
