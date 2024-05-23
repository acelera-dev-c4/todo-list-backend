using Domain.Models;
using Infra.DB;

namespace Infra.Repositories;

public interface ISubTaskRepository
{
    SubTask Create(SubTask subTask);
    List<SubTask> Get(int mainTaskId);
    SubTask? Find(int subTaskId);
    SubTask Update(SubTask subTask);
    void Delete(SubTask subTask);
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

    public SubTask Update(SubTask subTaskUpdate)
    {
        _myDBContext.SubTasks.Update(subTaskUpdate);
        _myDBContext.SaveChanges();
        return subTaskUpdate;
    }

    public void Delete(SubTask subTask)
    {
        _myDBContext.SubTasks.Remove(subTask);
    }
}