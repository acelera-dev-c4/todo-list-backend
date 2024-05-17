using AceleraDevTodoListApi.DB;
using Domain.Entitys;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AceleraDevTodoListApi.Infra.Repositories;

public interface ISubTarefaRepository{
    SubTarefa Create(SubTarefa subTarefa);
    SubTarefa? Get(int id);
    List<SubTarefa> GetAll();
    SubTarefa Update(SubTarefa subTarefa, int id);
    void Delete(int id);
    }


public class SubTarefaRepository : ISubTarefaRepository{


    private readonly MyDBContext _myDBContext;
    public SubTarefaRepository(MyDBContext myDbContext){
        _myDBContext = myDbContext;
    }


    public SubTarefa Create(SubTarefa NovaSubTarefa){
        _myDBContext.SubTarefas.Add(NovaSubTarefa);
        _myDBContext.SaveChanges();
        return NovaSubTarefa;
    }
    public SubTarefa? Get(int idSubtarefa){
        
        return _myDBContext.SubTarefas.FirstOrDefault();
    }

    public List<SubTarefa> GetAll(){
        return _myDBContext.SubTarefas.ToList();
    }

    public SubTarefa Update(SubTarefa SubTarefaUpdate, int id){

        _myDBContext.SubTarefas.Update(SubTarefaUpdate);
        _myDBContext.SaveChanges();
        return SubTarefaUpdate;
    }

    public void Delete(int id){
        _myDBContext.SubTarefas.Where(x => x.Id == id).ExecuteDelete();
    }
}