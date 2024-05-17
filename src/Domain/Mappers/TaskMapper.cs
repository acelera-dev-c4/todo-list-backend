using Domain.Models;
using Domain.Request;

namespace Domain.Mappers;

public class TaskMapper
{
    public static Tarefas ToClass(TaskRequest task) => new Tarefas
    {
        Id = null,
        IdUsuario = task.UserId,
        Descricao = task.Description
    };
}
