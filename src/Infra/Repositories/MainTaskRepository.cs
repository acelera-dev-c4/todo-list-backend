using Domain.Models;
using Infra.DB;

namespace Infra.Repositories;

public interface IMainTaskRepository
{
    MainTask Create(MainTask mainTask);
    List<MainTask> Get(int userId);
    MainTask? Find(int mainTaskId);
    MainTask Update(MainTask mainTask);
    void Delete(MainTask mainTask);
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

    public MainTask Update(MainTask mainTaskUpdate)
    {
        _myDBContext.MainTasks.Update(mainTaskUpdate);
        _myDBContext.SaveChanges();
        return mainTaskUpdate;
    }

    public void Delete(MainTask mainTask)
    {
        //_myDBContext.SubTasks.Where(x => x.MainTaskId == mainTaskId).ExecuteDelete();
        //_myDBContext.MainTasks.Where(x => x.Id == mainTaskId).ExecuteDelete();
        _myDBContext.MainTasks.Remove(mainTask);
    }
}