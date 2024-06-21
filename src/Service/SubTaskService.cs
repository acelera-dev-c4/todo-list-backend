﻿using Domain.Exceptions;
using Domain.Mappers;
using Domain.Models;
using Infra;
using Infra.DB;
using Infra.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Service;

public interface ISubTaskService
{
    SubTask Create(SubTaskRequest subTaskRequest);
    List<SubTask> List(int mainTaskId);
    Task<SubTask> Update(SubTaskUpdate subTaskUpdate, int subTaskId);
    void Delete(int subTaskId);
    Task SetCompletedOrNot(int mainTaskId, int userIdSubscriber);
    Task<bool> VerifyFinished(int mainTaskId, int userIdSubscriber);
    Task UpdateSubtaskFinished(int subTaskId, bool finishedSubTask);
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

    private bool IsMainTaskInSubscriptions(int mainTaskId)
    {
        var mainTask = _myDBContext.MainTasks.FirstOrDefault(m => m.Id == mainTaskId);

        if (mainTask is null) throw new NotFoundException("Maintask not found!");

        return mainTask.UrlNotificationWebhook.IsNullOrEmpty() ? false : true;
    }

    public SubTask Create(SubTaskRequest subTaskRequest)
    {
        var newSubTask = SubTaskMapper.ToClass(subTaskRequest);
        return _subTaskRepository.Create(newSubTask);
    }

    public void Delete(int subTaskId)
    {
        var subTask = _subTaskRepository.Find(subTaskId);

        if (subTask is null)
            throw new Exception("subTask not found!");

        var mainTask = _mainTaskRepository.Find(subTask.MainTaskId);
        if (mainTask is null)
            throw new Exception("mainTask not found!");

        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null || mainTask.UserId.ToString() != userId)
        {
            throw new UnauthorizedAccessException("You don't have permission to delete this subtask.");
        }

        _subTaskRepository.Delete(subTaskId);
    }

    public List<SubTask> List(int mainTaskId)
    {
        return _subTaskRepository.Get(mainTaskId);
    }

    public async Task<SubTask> Update(SubTaskUpdate updateSubTaskRequest, int subTaskId)
    {
        var subTask = _subTaskRepository.Find(subTaskId) ?? throw new NotFoundException("SubTask not found!");

        var mainTask = _mainTaskRepository.Find(subTask.MainTaskId) ?? throw new NotFoundException("MainTask not found!");

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

        var mainTaskId = subTask.MainTaskId;

        var taskAssinante = _mainTaskRepository.Find(mainTaskId) ?? throw new NotFoundException("Maintask not found!");

        subTask.Description = updateSubTaskRequest.Description;
        subTask.Finished = updateSubTaskRequest.Finished;
        await SetCompletedOrNot(subTask.MainTaskId, taskAssinante.UserId);
        return await _subTaskRepository.Update(subTask);
    }

    /// <summary>
    /// Verifies if all subTasks of a mainTask are completed
    /// </summary>
    /// <param name="mainTaskId"></param>
    /// <returns></returns>
    public async Task<bool> VerifyFinished(int mainTaskId, int userIdSubscriber)
    {
        var list = List(mainTaskId);
        foreach (var item in list)
        {
            if (!item.Finished)
                return false;
        }

        //var userToken = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
        var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (token is null) throw new BadRequestException("Invalid user authentication");

        if (IsMainTaskInSubscriptions(mainTaskId))
        {
            var subscriptions = await _notificationHttpClient.GetSubscriptionsByMainTaskId(mainTaskId, token);
            var mainTask = _mainTaskRepository.Find(mainTaskId);

            foreach (Subscription subscription in subscriptions)
            {
                await _notificationHttpClient.CreateNotification(token,
                    (int)subscription.Id!,
                    "A tarefa " + mainTask!.Description + " foi concluida.",
                    false,
                    userIdSubscriber
                    );
            }
        }

        return true;
    }

    /// <summary>
    /// Sets a mainTask as completed or not completed.
    /// </summary>
    /// <param name="mainTaskId"></param>
    public async Task SetCompletedOrNot(int mainTaskId, int userIdSubscriber)
    {
        _mainTaskService.Find(mainTaskId)!.Completed = await VerifyFinished(mainTaskId, userIdSubscriber);
        await _myDBContext.SaveChangesAsync();
    }

    public async Task UpdateSubtaskFinished(int subTaskId, bool finishedSubTask)
    {
        var subtask = await _subTaskRepository.UpdateSubtaskFinished(subTaskId, finishedSubTask);

        if (subtask is null)
            throw new NotFoundException("SubTask not found!");
    }
}
