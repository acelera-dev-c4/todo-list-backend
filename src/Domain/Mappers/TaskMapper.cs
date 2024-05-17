using Domain.Models;
using Domain.Request;

namespace Domain.Mappers;

public class TaskMapper
{
    public static Tarefa ToClass(TaskRequest task) => new Tarefa
    {
        Id = null,
        IdUsuario = task.UserId,
        Descricao = task.Description
    };
}
