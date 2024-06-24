using Domain.Exceptions;
using Domain.Mappers;
using Domain.Models;
using Infra;
using Infra.DB;
using Infra.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Service;

public interface ISubTaskService
{
    Task<SubTask> Create(SubTaskRequest subTaskRequest);
    Task<List<SubTask>> List(int mainTaskId);
    Task<SubTask> Update(SubTaskUpdate updateSubTaskRequest, int subTaskId);
    Task Delete(int subTaskId);
    Task SetMainTaskCompletedOrNot(int mainTaskId);
    Task<bool> VerifyFinished(int mainTaskId);
    Task<SubTask> UpdateSubtaskFinished(int subTaskId, bool finishedSubTask);
}
public class SubTaskService : ISubTaskService
{
    private readonly ISubTaskRepository _subTaskRepository;
    private readonly IMainTaskRepository _mainTaskRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly MyDBContext _myDBContext;
    private readonly IMainTaskService _mainTaskService;
    private readonly NotificationHttpClient _notificationHttpClient;
    public SubTaskService(ISubTaskRepository subTaskRepository,
                          IMainTaskRepository mainTaskRepository,
                          IHttpContextAccessor httpContextAccessor,
                          MyDBContext myDbContext,
                          IMainTaskService mainTaskService,
                          NotificationHttpClient notificationHttpClient
                          )
    {
        _subTaskRepository = subTaskRepository;
        _mainTaskRepository = mainTaskRepository;
        _httpContextAccessor = httpContextAccessor;
        _myDBContext = myDbContext;
        _mainTaskService = mainTaskService;
        _notificationHttpClient = notificationHttpClient;

    }
    private async Task<bool> IsSubTaskInSubscriptions(int subTaskId)
    {
        var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (token is null) throw new BadRequestException("Invalid user authentication");
        var subscriptions = await _notificationHttpClient.GetSubscriptionsBySubTaskId(subTaskId, token);

        return subscriptions.IsSuccessStatusCode;
    }

    private async Task<bool> IsMainTaskInSubscriptions(int mainTaskId)
    {
        var mainTask = await _myDBContext.MainTasks.FirstOrDefaultAsync(m => m.Id == mainTaskId);

        if (mainTask is null) throw new NotFoundException("Maintask not found!");

        return mainTask.UrlNotificationWebhook.IsNullOrEmpty() ? false : true;
    }

    public async Task<SubTask> Create(SubTaskRequest subTaskRequest)
    {
        var newSubTask = SubTaskMapper.ToClass(subTaskRequest);
        return await _subTaskRepository.Create(newSubTask);
    }

    public async Task Delete(int subTaskId)
    {
        var subTask = await _subTaskRepository.Find(subTaskId);

        if (subTask is null)
            throw new NotFoundException("SubTask not found!");

        var mainTask = await _mainTaskRepository.Find(subTask.MainTaskId);
        if (mainTask is null)
            throw new NotFoundException("MainTask not found!");

        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null || mainTask.UserId.ToString() != userId)
        {
            throw new UnauthorizedAccessException("You don't have permission to delete this subtask.");
        }

        await _subTaskRepository.Delete(subTaskId);
    }

    public async Task<List<SubTask>> List(int mainTaskId)
    {
        return await _subTaskRepository.Get(mainTaskId);
    }

    public async Task<SubTask> Update(SubTaskUpdate updateSubTaskRequest, int subTaskId) // Apenas para chamadas da NotificationApi pq email system user
    {
        var subTask = await _subTaskRepository.Find(subTaskId) ?? throw new NotFoundException("SubTask not found!");
        var mainTask = await _mainTaskRepository.Find(subTask.MainTaskId) ?? throw new NotFoundException("MainTask not found!");
        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userEmail = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;

        if (userEmail != "system@mail.com")
        {
            if (userId != mainTask.UserId.ToString()) throw new UnauthorizedAccessException("You don't have permission to update this subtask.");
        }

        //Se a subtarefa está presente na tabela subscriptions
        if (await IsSubTaskInSubscriptions(subTaskId))
        {
            //se a pessoa que criou, é a mesma que esta tentando dar update.
            if (userId == mainTask.UserId.ToString())
                throw new BadRequestException("This task cannot be completed beacuse it has an active sub");
        }

        subTask.Description = updateSubTaskRequest.Description;
        subTask.Finished = updateSubTaskRequest.Finished;
        await SetMainTaskCompletedOrNot(subTask.MainTaskId);
        return await _subTaskRepository.Update(subTask);
    }

    /// <summary>
    /// Verifies if all subTasks of a mainTask are completed
    /// </summary>
    /// <param name="mainTaskId"></param>
    /// <returns></returns>
    public async Task<bool> VerifyFinished(int mainTaskId)
    {
        var list = await List(mainTaskId);
        foreach (var item in list)
        {
            if (!item.Finished)
                return false;
        }

        var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (token is null) throw new BadRequestException("Invalid user authentication");

        if (await IsMainTaskInSubscriptions(mainTaskId))
        {
            var subscriptions = await _notificationHttpClient.GetSubscriptionsByMainTaskId(mainTaskId, token);

            var mainTask = await _mainTaskRepository.Find(mainTaskId);

            foreach (Subscription subscription in subscriptions)
            {
                var subtaskSubscription = await _subTaskRepository.Find(subscription.SubTaskIdSubscriber);
                var maintaskSubscription = await _mainTaskRepository.Find(subtaskSubscription!.MainTaskId);

                await _notificationHttpClient.CreateNotification(token,
                                                                 (int)subscription.Id!,
                                                                 $"A tarefa '{mainTask!.Description}' foi concluida.",
                                                                 false,
                                                                 maintaskSubscription!.UserId,
                                                                 mainTask.UrlNotificationWebhook
                                                                 );
            }
        }

        return true;
    }

    /// <summary>
    /// Sets a mainTask as completed or not completed.
    /// </summary>
    /// <param name="mainTaskId"></param>
    public async Task SetMainTaskCompletedOrNot(int mainTaskId)
    {
        var mainTask = await _mainTaskService.Find(mainTaskId) ?? throw new NotFoundException("Maintask not found!");

        mainTask.Completed = await VerifyFinished(mainTaskId);

        await _myDBContext.SaveChangesAsync();
    }

    public async Task<SubTask> UpdateSubtaskFinished(int subTaskId, bool finishedSubTask)
    {
        var subTask = await _subTaskRepository.Find(subTaskId) ?? throw new NotFoundException("SubTask not found!");
        SubTaskUpdate request = new()
        {
            Description = subTask.Description,
            Finished = finishedSubTask
        };        
        var mainTask = await _mainTaskRepository.Find(subTask.MainTaskId) ?? throw new NotFoundException("MainTask not found!");
        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userEmail = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;

        if (userEmail != "system@mail.com")
        {
            if (userId != mainTask.UserId.ToString()) throw new UnauthorizedAccessException("You don't have permission to update this subtask.");
        }
        //Se a subtarefa está presente na tabela subscriptions
        if (await IsSubTaskInSubscriptions(subTaskId) == true)
        {
            //se a pessoa que criou, é a mesma que esta tentando dar update.
            if (userId == mainTask.UserId.ToString())
                throw new BadRequestException("This task cannot be completed beacuse it has an active sub");
        }

        subTask.Description = request.Description;
        subTask.Finished = request.Finished;
        await SetMainTaskCompletedOrNot(subTask.MainTaskId);
        return await _subTaskRepository.Update(subTask);
    }
}
