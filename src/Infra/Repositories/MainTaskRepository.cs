using Infra.DB;
using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace Infra.Repositories;

public interface IMainTaskRepository
{
    MainTask Create(MainTask mainTask);
    List<MainTask> Get(int userId);
    MainTask Update(MainTask mainTask, int mainTaskId);
    void Delete(int mainTaskId);
}

public class MainTaskRepository : IMainTaskRepository
{
    private readonly MyDBContext _myDBContext;

    public MainTaskRepository(MyDBContext myDBContext)
    {
        _myDBContext = myDBContext;
    }

    public MainTask Create(MainTask mainTask)
    {
        _myDBContext.MainTasks.Add(mainTask);
        _myDBContext.SaveChanges();
        return mainTask;
    }

    public List<MainTask> Get(int userId)
    {
        return _myDBContext.MainTasks.Where(x => x.UserId == userId).ToList();
    }

    public MainTask Update(MainTask mainTaskUpdate, int mainTaskId)
    {
        if (_myDBContext.MainTasks.Find(mainTaskId) is null)
        {
            throw new Exception("SubTask not found!");
        }

        _myDBContext.MainTasks.Update(mainTaskUpdate);
        _myDBContext.SaveChanges();
        return mainTaskUpdate;
    }

    public void Delete(int mainTaskId)
    {
        _myDBContext.MainTasks.Where(x => x.Id == mainTaskId).ExecuteDelete();
    }
}