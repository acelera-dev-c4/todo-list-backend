using Infra.DB;
using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace Infra.Repositories;

public interface ITarefaRepository
{
    Tarefa Create(Tarefa Tarefa);
    List<Tarefa> Get(int idUsuario);
    Tarefa? Find(int idUsuario);
    Tarefa Update(Tarefa Tarefa, int idTarefa);
    void Delete(int idTarefa);
}

public class TarefaRepository : ITarefaRepository
{
    private readonly MyDBContext _myDBContext;

    public TarefaRepository(MyDBContext myDBContext)
    {
        _myDBContext = myDBContext;
    }

    public Tarefa Create(Tarefa NovaTarefa)
    {
        _myDBContext.Tarefas.Add(NovaTarefa);
        _myDBContext.SaveChanges();
        return NovaTarefa;
    }

    public List<Tarefa> Get(int idUsuario)
    {
        return _myDBContext.Tarefas.Where(x => x.IdUsuario == idUsuario).ToList();
    }

    public Tarefa? Find(int idTarefa)
    {
        return _myDBContext.Tarefas.Find(idTarefa);
    }

    public Tarefa Update(Tarefa TarefaUpdate, int idTarefa)
    {
        if (_myDBContext.Tarefas.Find(idTarefa) is null)
        {
            throw new Exception("Tarefa n�o encontrada para atualiza��o");
        }

        _myDBContext.Tarefas.Update(TarefaUpdate);
        _myDBContext.SaveChanges();
        return TarefaUpdate;
    }

    public void Delete(int idTarefa)
    {
        _myDBContext.Tarefas.Where(x => x.Id == idTarefa).ExecuteDelete();
    }
}