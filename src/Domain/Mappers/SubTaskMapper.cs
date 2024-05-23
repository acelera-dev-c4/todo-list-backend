﻿using Domain.Models;
using Domain.Request;

namespace Domain.Mappers;

public class SubTaskMapper
{
    public static SubTask ToClass(SubTaskRequest subTaskRequest) => new SubTask
    {
        Id = null,
        MainTaskId = subTaskRequest.MainTaskId,
        Description = subTaskRequest.Description,
        Finished = subTaskRequest.Finished
    };

    public static SubTask ToUpdate(SubTaskUpdate subTaskUpdate, int mainTaskId) => new SubTask
    {
        Id = null,
        MainTaskId = mainTaskId,
        Description = subTaskUpdate.Description,
        Finished = subTaskUpdate.Finished
    };
}
