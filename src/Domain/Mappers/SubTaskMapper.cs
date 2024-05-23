using Domain.Models;

namespace Domain.Mappers;

public class SubTaskMapper
{
    public static SubTask ToClass(SubTaskRequest subTaskRequest) => new SubTask
    {
        Id = null,
        MainTaskId = subTaskRequest.MainTaskId,
        Description = subTaskRequest.Description,
        Finished = false
    };

    public static SubTask ToUpdate(SubTaskUpdate subTaskUpdate, int mainTaskId) => new SubTask
    {
        Id = null,
        MainTaskId = mainTaskId,
        Description = subTaskUpdate.Description,
        Finished = subTaskUpdate.Finished
    };
}
