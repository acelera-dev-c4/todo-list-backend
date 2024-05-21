using Domain.Entitys;
using Microsoft.EntityFrameworkCore;
using AceleraDevTodoListApi.DB;

namespace AceleraDevTodoListApi.Infra.Repositories;

public interface ISubTarefaRepository
{
    SubTarefa Create(SubTarefa subTarefa);
    List<SubTarefa> Get(int idTarefa);
    SubTarefa? Find(int idSubTarefa);
    SubTarefa Update(SubTarefa subTarefa, int idSubTarefa);
    void Delete(int idSubTarefa);
}


public class SubTarefaRepository : ISubTarefaRepository
{
    private readonly MyDBContext _myDBContext;

    public SubTarefaRepository(MyDBContext myDbContext)
    {
        _myDBContext = myDbContext;
    }

    public SubTarefa Create(SubTarefa NovaSubTarefa)
    {
        _myDBContext.SubTarefas.Add(NovaSubTarefa);
        _myDBContext.SaveChanges();
        return NovaSubTarefa;
    }

    public List<SubTarefa> Get(int idTarefa)
    {
        return _myDBContext.SubTarefas.Where(x => x.IdTarefa == idTarefa).ToList();
    }

    public SubTarefa? Find(int idSubTarefa)
    {
        return _myDBContext.SubTarefas.Find(idSubTarefa);
    }

    public SubTarefa Update(SubTarefa SubTarefaUpdate, int idSubTarefa)
    {
        _myDBContext.SubTarefas.Update(SubTarefaUpdate);
        _myDBContext.SaveChanges();
        return SubTarefaUpdate;
    }

    public void Delete(int idSubTarefa)
    {
        _myDBContext.SubTarefas.Where(x => x.Id == idSubTarefa).ExecuteDelete();
    }
}