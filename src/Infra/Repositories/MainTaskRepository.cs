using Domain.Models;
using Infra.DB;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public interface IMainTaskRepository
{
    Task<MainTask> Create(MainTask mainTask);
    Task<List<MainTask>> GetAll();
    Task<List<MainTask>> Get(int userId);
    Task<MainTask?> Find(int mainTaskId);
    Task<List<MainTask>> FindByDescription(string desc);
    Task<MainTask> Update(MainTask mainTask);
    Task Delete(int mainTaskId);
}

public class MainTaskRepository : IMainTaskRepository
{
    private readonly MyDBContext _myDBContext;

    public MainTaskRepository(MyDBContext myDBContext)
    {
        _myDBContext = myDBContext;
    }

    public async Task<MainTask> Create(MainTask mainTask)
    {
        await _myDBContext.MainTasks.AddAsync(mainTask);
        await _myDBContext.SaveChangesAsync();
        return mainTask;
    }

    public async Task<List<MainTask>> Get(int userId)
    {
        return await _myDBContext.MainTasks.Where(x => x.UserId == userId).ToListAsync();
    }
    public async Task<List<MainTask>> GetAll()
    {
        return await _myDBContext.MainTasks.ToListAsync();
    }
    public async Task<MainTask?> Find(int mainTaskId)
    {
        return await _myDBContext.MainTasks.FindAsync(mainTaskId);
    }

    public async Task<List<MainTask>> FindByDescription(string desc)
    {
        return await _myDBContext.MainTasks.Where(x => x.Description!.Contains(desc)).ToListAsync();
    }

    public async Task<MainTask> Update(MainTask mainTaskUpdate)
    {
        await _myDBContext.MainTasks
            .Where(mt => mt.Id == mainTaskUpdate.Id)
            .ExecuteUpdateAsync(mt => mt
                .SetProperty(maintask => maintask.Description, mainTaskUpdate.Description)
            );
        await _myDBContext.SaveChangesAsync();
        return mainTaskUpdate;
    }

    public async Task Delete(int mainTaskId)
    {
        await _myDBContext.MainTasks.Where(x => x.Id == mainTaskId).ExecuteDeleteAsync();
    }

}
