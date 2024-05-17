using AceleraDevTodoListApi.DB;
using Domain.Entitys;
using Microsoft.EntityFrameworkCore;

namespace AceleraDevTodoListApi.Infra.Repositories;

public interface ITarefaRepository{
    Tarefa Create(Tarefa Tarefa);
    Tarefa? Get(int id);
    List<Tarefa> GetAll();
    Tarefa Update(Tarefa Tarefa, int id);
    void Delete(int id);
}

public class TarefaRepository : ITarefaRepository{

    private readonly MyDBContext _myDBContext;
    public TarefaRepository(MyDBContext myDBContext){
        _myDBContext = myDBContext;
    }

    public Tarefa Create(Tarefa NovaTarefa) {

        _myDBContext.Tarefas.Add(NovaTarefa);
        _myDBContext.SaveChanges();
        return NovaTarefa;
    }
    public Tarefa? Get(int idTarefa) {
        return _myDBContext.Tarefas.Find(idTarefa);
    }
    public List<Tarefa> GetAll()
    {
        return _myDBContext.Tarefas.ToList();
    }
    public Tarefa Update(Tarefa TarefaUpdate, int id) {
        _myDBContext.Tarefas.Update(TarefaUpdate);
        _myDBContext.SaveChanges();
        return TarefaUpdate;
    }
    public void Delete(int id) {
        _myDBContext.Tarefas.Where(x => x.Id == id).ExecuteDelete();
    }

}