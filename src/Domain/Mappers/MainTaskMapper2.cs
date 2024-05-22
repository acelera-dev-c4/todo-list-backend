using Domain.Models;
using Domain.Request;

namespace Domain.Mappers;

public class MainTaskMapper2
{
    public static MainTask ToClass(MainTaskRequest task) => new MainTask
    {
        Id = null,
        UserId = task.UserId,
        Description = task.Description
    };
}
