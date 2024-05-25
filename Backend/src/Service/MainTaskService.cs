using Domain.Mappers;
using Domain.Models;
using Domain.Request;
using Infra.Repositories;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Service;

public interface IMainTaskService
{
    MainTask Create(MainTaskRequest mainTask);
    void Delete(int mainTaskId);
    List<MainTask>? Get(int userId);
    MainTask? Find(int mainTaskId);
    MainTask Update(MainTaskUpdate mainTask, int mainTaskId);
}

public class MainTaskService : IMainTaskService
{
    private readonly IMainTaskRepository _mainTaskRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public MainTaskService(IMainTaskRepository mainTaskRepository, IHttpContextAccessor httpContextAccessor)
    {
        _mainTaskRepository = mainTaskRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public MainTask Create(MainTaskRequest mainTaskRequest)
    {
        var newMainTask = MainTaskMapper.ToClass(mainTaskRequest);
        return _mainTaskRepository.Create(newMainTask);
    }

    public List<MainTask>? Get(int userId)
    {
        return _mainTaskRepository.Get(userId);
    }

    public MainTask? Find(int mainTaskId)
    {
        var mainTask = _mainTaskRepository.Find(mainTaskId);

        if (mainTask is null)
            throw new Exception("mainTask not found!");

        return mainTask;
    }

    public MainTask Update(MainTaskUpdate mainTaskUpdate, int mainTaskId)
    {
        var mainTask = _mainTaskRepository.Find(mainTaskId);

        if (mainTask is null)
            throw new Exception("mainTask not found!");

        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (mainTask.UserId.ToString() != userId)
        {
            throw new UnauthorizedAccessException("You don't have permission to update this task.");
        }

        mainTask.Description = mainTaskUpdate.Description;

        return _mainTaskRepository.Update(mainTask);
    }

    public void Delete(int mainTaskId)
    {
        var mainTask = _mainTaskRepository.Find(mainTaskId);

        if (mainTask is null)
            throw new Exception("mainTask not found!");

        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (mainTask.UserId.ToString() != userId)
        {
            throw new UnauthorizedAccessException("You don't have permission to delete this task.");
        }

        _mainTaskRepository.Delete(mainTaskId);
    }
}
