using Domain.Exceptions;
using Domain.Mappers;
using Domain.Models;
using Domain.Request;
using Infra;
using Infra.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;


namespace Service;

public interface IMainTaskService
{
    Task<MainTask> Create(MainTaskRequest mainTask);

    Task Delete(int mainTaskId);
    Task<List<MainTask>?> Get(int userId);
    Task<MainTask?> Find(int mainTaskId);
    Task<MainTask> Update(MainTaskUpdate mainTask, int mainTaskId);
    Task<List<MainTask>?> SearchByParams(int? mainTaskId, string? userName, string? mainTaskDescription);
    Task SetUrlWebhook(int mainTaskId, string url);
    Task<MainTask> UpdateUrl(MainTaskUpdate mainTaskUpdate, int mainTaskId);
}

public class MainTaskService : IMainTaskService
{
    private readonly IMainTaskRepository _mainTaskRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserService _userService;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly NotificationHttpClient _notificationClient;

    public MainTaskService(IMainTaskRepository mainTaskRepository, IHttpContextAccessor httpContextAccessor, IUserService userService, IHttpClientFactory httpClientFactory)
    {
        _mainTaskRepository = mainTaskRepository;
        _httpContextAccessor = httpContextAccessor;
        _userService = userService;
        _httpClientFactory = httpClientFactory;
        _notificationClient = new(_httpClientFactory);
    }

    public async Task<MainTask> Create(MainTaskRequest mainTaskRequest)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId != mainTaskRequest.UserId.ToString()) throw new UnauthorizedAccessException("You don't have permission to create this task.");

        var newMainTask = MainTaskMapper.ToClass(mainTaskRequest);
        return await _mainTaskRepository.Create(newMainTask);
    }

    public async Task<List<MainTask>?> Get(int userId)
    {
        return await _mainTaskRepository.Get(userId);
    }

    public async Task<MainTask?> Find(int mainTaskId)
    {
        var mainTask = await _mainTaskRepository.Find(mainTaskId);

        if (mainTask is null)
            throw new NotFoundException("MainTask not found!");

        return mainTask;
    }

    public async Task<MainTask> Update(MainTaskUpdate mainTaskUpdate, int mainTaskId)
    {
        var mainTask = await _mainTaskRepository.Find(mainTaskId);

        if (mainTask is null)
            throw new NotFoundException("mainTask not found!");

        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (mainTask.UserId.ToString() != userId)
        {
            throw new UnauthorizedAccessException("You don't have permission to update this task.");
        }

        mainTask.Description = mainTaskUpdate.Description;
        return await _mainTaskRepository.Update(mainTask);
    }

    public async Task<MainTask> UpdateUrl(MainTaskUpdate mainTaskUpdate, int mainTaskId) // metodo exclusivo para o notification api
    {
        var mainTask = await _mainTaskRepository.Find(mainTaskId);

        if (mainTask is null)
            throw new NotFoundException("MainTask not found!");

        var userEmail = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;

        if (userEmail != "system@mail.com")
        {
            throw new UnauthorizedAccessException("You are not authorized to change Url value ");
        }

        mainTask.UrlNotificationWebhook = mainTaskUpdate.UrlNotificationWebhook!;
        return await _mainTaskRepository.Update(mainTask);
    }

    public async Task Delete(int mainTaskId)
    {
        var mainTask = await _mainTaskRepository.Find(mainTaskId);

        if (mainTask is null)
            throw new NotFoundException("MainTask not found!");

        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (mainTask.UserId.ToString() != userId)
        {
            throw new UnauthorizedAccessException("You don't have permission to delete this task.");
        }

        await _mainTaskRepository.Delete(mainTaskId);
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
            MainTask? foundById = await _mainTaskRepository.Find((int)mainTaskId!);

            if (foundById != null)
            {
                if (!result.Contains(foundById))
                    result.Add(foundById);
            }
        }

        if (validMainTaskDescription)
        {
            List<MainTask>? tasksByDesc = await _mainTaskRepository.FindByDescription(mainTaskDescription!);
            foreach (var task in tasksByDesc)
            {
                if (!result.Contains(task))
                    result.Add(task);
            }
        }

        if (validUserName)
        {
            foundUsers = await _userService.GetByName(userName!);

            if (!foundUsers.IsNullOrEmpty())
            {
                foreach (var user in foundUsers!)
                {
                    var listFromUser = await Get(user.Id);
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

    public async Task SetUrlWebhook(int mainTaskId, string url)
    {
        var task = await _mainTaskRepository.Find(mainTaskId);
        if (task != null)
        {
            MainTaskUpdate updated = new();
            updated.Description = task.Description;
            updated.UrlNotificationWebhook = url;
            await UpdateUrl(updated, mainTaskId);
        }
        else
            throw new NotFoundException("MainTask Not Found, url to notify was not updated");
    }
}
