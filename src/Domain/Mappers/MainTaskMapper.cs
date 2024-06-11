using Domain.Models;
using Domain.Request;

namespace Domain.Mappers;

public class MainTaskMapper
{
    public static MainTask ToClass(MainTaskRequest task) => new MainTask
    {
        Id = null,
        UserId = task.UserId,
        Description = task.Description,
        Completed = false
    };
}
