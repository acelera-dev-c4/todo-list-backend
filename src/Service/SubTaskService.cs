using Domain.Models;
using Infra.Repositories;

namespace Service;

public class SubTaskService
{
    private readonly ISubTaskRepository _subTaskRepository;

    public SubTaskService(ISubTaskRepository subTaskRepository)
    {
        _subTaskRepository = subTaskRepository;
    }

    public SubTask Create(SubTask subTask)
    {
        return _subTaskRepository.Create(subTask);
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

    public SubTask Update(SubTask subTaskUpdate, int subTaskId)
    {
        return _subTaskRepository.Update(subTaskUpdate, subTaskId);
    }
}
