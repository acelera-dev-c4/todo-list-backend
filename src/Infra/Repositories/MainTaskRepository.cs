using Domain.Models;
using Infra.DB;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public interface IMainTaskRepository
{
    MainTask Create(MainTask mainTask);
    List<MainTask> Get(int userId);
    MainTask? Find(int mainTaskId);
    List<MainTask> FindByDescription(string desc);
    MainTask Update(MainTask mainTask);
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

    public MainTask? Find(int mainTaskId)
    {
        return _myDBContext.MainTasks.Find(mainTaskId);
    }

    public List<MainTask> FindByDescription(string desc)
    {
        return _myDBContext.MainTasks.Where(x => x.Description!.Contains(desc)).ToList();
    }

    public MainTask Update(MainTask mainTaskUpdate)
    {
        _myDBContext.MainTasks.Update(mainTaskUpdate);
        _myDBContext.SaveChanges();
        return mainTaskUpdate;
    }

    public void Delete(int mainTaskId)
    {
        _myDBContext.MainTasks.Where(x => x.Id == mainTaskId).ExecuteDelete();
    }
}