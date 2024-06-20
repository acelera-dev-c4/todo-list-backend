using Domain.Mappers;
using Domain.Models;
using Domain.Request;
using Infra.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Service;

public interface IMainTaskService
{
    Task<MainTask> Create(MainTaskRequest mainTask);
    void Delete(int mainTaskId);
    List<MainTask>? Get(int userId);
    MainTask? Find(int mainTaskId);
    MainTask Update(MainTaskUpdate mainTask, int mainTaskId);
    Task<List<MainTask>?> SearchByParams(int? mainTaskId, string? userName, string? mainTaskDescription);
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

    public async Task<MainTask> Create(MainTaskRequest mainTaskRequest)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId != mainTaskRequest.UserId.ToString()) throw new UnauthorizedAccessException("You don't have permission to create this task.");

        var newMainTask = MainTaskMapper.ToClass(mainTaskRequest);
        return await _mainTaskRepository.Create(newMainTask);
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

    public async Task<List<MainTask>?> SearchByParams(int? mainTaskId, string? userName, string? mainTaskDescription)
    {
        List<MainTask>? result = new();
        List<User>? foundUsers = new();
        bool validMainTaskId = mainTaskId != null;
        bool validUserName = !userName.IsNullOrEmpty();
        bool validMainTaskDescription = !mainTaskDescription.IsNullOrEmpty();


        if (validMainTaskId)
        {
            MainTask? foundById = _mainTaskRepository.Find((int)mainTaskId!);

            if (foundById != null)
            {
                if (!result.Contains(foundById))
                    result.Add(foundById);
            }
        }

        if (validMainTaskDescription)
        {
            List<MainTask>? tasksByDesc = _mainTaskRepository.FindByDescription(mainTaskDescription!);
            foreach (var task in tasksByDesc)
            {
                if (!result.Contains(task))
                    result.Add(task);
            };
        }

        if (validUserName)
        {
            foundUsers = await _userService.GetByName(userName!);

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
        }

        return result;
    }
}
