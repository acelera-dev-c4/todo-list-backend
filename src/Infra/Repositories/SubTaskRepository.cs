using Domain.Models;
using Infra.DB;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public interface ISubTaskRepository
{
    Task<SubTask> Create(SubTask newSubTask);
    Task<List<SubTask>> Get(int mainTaskId);
    Task<SubTask?> Find(int subTaskId);
    Task<SubTask> Update(SubTask subTask);
    Task Delete(int subTaskId);
    Task<SubTask> UpdateSubtaskFinished(int subTaskId, bool finishedSubTask);

}

public class SubTaskRepository : ISubTaskRepository
{
    private readonly MyDBContext _myDBContext;


    public SubTaskRepository(MyDBContext myDbContext)
    {
        _myDBContext = myDbContext;
    }

    public async Task<SubTask> Create(SubTask newSubTask)
    {
        await _myDBContext.SubTasks.AddAsync(newSubTask);
        await _myDBContext.SaveChangesAsync();
        return newSubTask;
    }

    public async Task<List<SubTask>> Get(int mainTaskId)
    {
        return await _myDBContext.SubTasks.Where(x => x.MainTaskId == mainTaskId).ToListAsync();
    }

    public async Task<SubTask?> Find(int subTaskId)
    {
        return await _myDBContext.SubTasks.FindAsync(subTaskId);
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

    public async Task Delete(int subTaskId)
    {
        await _myDBContext.SubTasks.Where(x => x.Id == subTaskId).ExecuteDeleteAsync();
    }

    public async Task<SubTask> UpdateSubtaskFinished(int subTaskId, bool finishedSubTask)
    {
        var subTask = await _myDBContext.SubTasks.FindAsync(subTaskId);
        subTask!.Finished = finishedSubTask;
        _myDBContext.SaveChanges();
        return subTask;
    }
}