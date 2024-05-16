using Domain.Entitys;
using Domain.Request;

namespace Domain.Mappers;

public class MapeadorTarefa
{
    public static Tarefa ParaClasse(RequisicaoTarefa tarefa) => new Tarefa
    {
        Id = null,
        IdUsuario = tarefa.IdUsuario,
        Descricao = tarefa.Descricao
    };
}
