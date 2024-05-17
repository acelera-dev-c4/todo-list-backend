using Domain.Models;
using Domain.Request;

namespace Domain.Mappers;

public class TaskMapper
{
    public static Tarefas ToClass(TaskRequest tarefas) => new Tarefas
    {
        Id = null,
        IdUsuario = tarefas.IdUsuario,
        Descricao = tarefas.Descricao
    };
}
