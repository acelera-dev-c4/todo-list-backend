using Domain.Models;
using Infra.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Infra.Repositories;

public interface ISubTaskRepository
{
    SubTask Create(SubTask subTask);
    List<SubTask> Get(int mainTaskId);
    SubTask? Find(int subTaskId);
    SubTask Update(SubTask subTask);
    void Delete(int subTaskId);
    bool VerifyFinished(int mainTaskId);    
}

public class SubTaskRepository : ISubTaskRepository
{
    private readonly MyDBContext _myDBContext;
    private readonly IMainTaskRepository _mainTaskRepository;

    public SubTaskRepository(MyDBContext myDbContext, IMainTaskRepository mainTaskRepository)
    {
        _myDBContext = myDbContext;
        _mainTaskRepository = mainTaskRepository;
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
        
        if (VerifyFinished(subTaskUpdate.MainTaskId))
            _mainTaskRepository.Complete(subTaskUpdate.MainTaskId);

        return subTaskUpdate;
    }

    public void Delete(int subTaskId)
    {
        _myDBContext.SubTasks.Where(x => x.Id == subTaskId).ExecuteDelete();
    }

    /// <summary>
    /// Verifies if all subTasks of a mainTask are completed
    /// </summary>
    /// <param name="mainTaskId"></param>
    /// <returns></returns>
    public bool VerifyFinished(int mainTaskId)
    {
        var list = Get(mainTaskId);
        int verifier = 0;
        foreach (var item in list)
        {
            if (!item.Finished)
                verifier = 1;            
        }
        return verifier == 0  ? true : false;
    }
}