using Infra.DB;
using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace Infra.Repositories;

public interface IMainTaskRepository
{
    MainTask Create(MainTask newMainTask);
    List<MainTask> Get(int userId);
    MainTask? Find(int userId);
    MainTask Update(MainTask updatedMainTask, int mainTaskId);
    void Delete(int mainTaskId);
}

public class MainTaskRepository : IMainTaskRepository
{
    private readonly MyDBContext _myDBContext;

    public MainTaskRepository(MyDBContext myDBContext)
    {
        _myDBContext = myDBContext;
    }

    public MainTask Create(MainTask newMainTask)
    {
        _myDBContext.MainTasks.Add(newMainTask);
        _myDBContext.SaveChanges();
        return newMainTask;
    }

    public List<MainTask> Get(int userId)
    {
        return _myDBContext.MainTasks.Where(x => x.UserId == userId).ToList();
    }

    public MainTask? Find(int mainTaskId)
    {
        return _myDBContext.MainTasks.Find(mainTaskId);
    }

    public MainTask Update(MainTask updatedMainTask, int mainTaskId)
    {
        if (_myDBContext.MainTasks.Find(mainTaskId) is null)
        {
            throw new Exception("MainTask n�o encontrada para atualiza��o");
        }

        _myDBContext.MainTasks.Update(updatedMainTask);
        _myDBContext.SaveChanges();
        return updatedMainTask;
    }

    public void Delete(int mainTaskId)
    {
        _myDBContext.MainTasks.Where(x => x.Id == mainTaskId).ExecuteDelete();
    }
}