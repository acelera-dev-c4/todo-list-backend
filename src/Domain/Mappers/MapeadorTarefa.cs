using Domain.Models;
using Domain.Request;

namespace Domain.Mappers;

public class MapeadorTarefa
{
    public static MainTask ParaClasse(RequisicaoTarefa tarefa) => new MainTask
    {
        Id = null,
        IdUsuario = tarefa.IdUsuario,
        Descricao = tarefa.Descricao
    };
}
