using Domain.Mappers;
using Domain.Models;
using Domain.Request;
using Infra.DB;
using Infra.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Service;

public interface IMainTaskService
{
    MainTask Create(MainTaskRequest mainTask);
    void Delete(int mainTaskId);
    List<MainTask>? Get(int userId);
    MainTask? Find(int mainTaskId);
    MainTask Update(MainTaskUpdate mainTask, int mainTaskId);
    List<MainTask>? GetByUserNameOrTaskDescription(string search);
}

public class MainTaskService : IMainTaskService
{
    private readonly IMainTaskRepository _mainTaskRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserService _userService;

    public MainTaskService(IMainTaskRepository mainTaskRepository, IHttpContextAccessor httpContextAccessor, IUserService userService)
    {
        _mainTaskRepository = mainTaskRepository;
        _httpContextAccessor = httpContextAccessor;
        _userService = userService;
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

    public List<MainTask>? GetByUserNameOrTaskDescription(string search)
    {
        List<User>? foundUsers = _userService.GetByName(search);
        List<MainTask>? tasksByDesc = _mainTaskRepository.FindByDescription(search);

        List<MainTask>? result = new();

        foreach (var task in tasksByDesc)
        { result.Add(task); };

        if (!foundUsers.IsNullOrEmpty())
        {
            foreach (var user in foundUsers!)
            {
                var listFromUser = Get(user.Id);
                foreach (var task in listFromUser!)
                {
                    if (!result.Contains(task))
                    {
                        result.Add(task);
                    }
                }
            }
        }

        return result;
    }
}
