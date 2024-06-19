using Domain.Models;
using Infra.DB;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public interface ISubTaskRepository
{
    SubTask Create(SubTask subTask);
    List<SubTask> Get(int mainTaskId);
    SubTask? Find(int subTaskId);
    Task<SubTask> Update(SubTask subTask);
    void Delete(int subTaskId);
}

public class SubTaskRepository : ISubTaskRepository
{
    private readonly MyDBContext _myDBContext;


    public SubTaskRepository(MyDBContext myDbContext)
    {
        _myDBContext = myDbContext;
    }

    public SubTask Create(SubTask newSubTask)
    {
        _myDBContext.SubTasks.Add(newSubTask);
        _myDBContext.SaveChanges();
        return newSubTask;
    }

    public List<SubTask> Get(int mainTaskId)
    {
        return _myDBContext.SubTasks.Where(x => x.MainTaskId == mainTaskId).ToList();
    }

    public SubTask? Find(int subTaskId)
    {
        return _myDBContext.SubTasks.Find(subTaskId);
    }

    public async Task<SubTask> Update(SubTask subTaskUpdate)
    {
        await _myDBContext.SubTasks
             .Where(st => st.Id == subTaskUpdate.Id)
             .ExecuteUpdateAsync(st => st
                 .SetProperty(subTask => subTask.MainTaskId, subTaskUpdate.MainTaskId)
                 .SetProperty(subTask => subTask.Description, subTaskUpdate.Description)
                 .SetProperty(subTask => subTask.Finished, subTaskUpdate.Finished)
             );

        await _myDBContext.SaveChangesAsync();
        return subTaskUpdate;
    }

    public void Delete(int subTaskId)
    {
        _myDBContext.SubTasks.Where(x => x.Id == subTaskId).ExecuteDelete();
    }
}