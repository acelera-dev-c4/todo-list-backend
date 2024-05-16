namespace AceleraDevTodoListApi.DB;

public interface ISubTarefaRepository(){
    SubTarefa Create(SubTarefa subTarefa);
    SubTarefa Get(int id);
    List<SubTarefa> GetAll();
    SubTarefa Update(SubTarefa subTarefa, int id);
    void Delete(int id);
    }


public class SubTarefaRepository : ISubTarefaRepository{


    private readonly MyDBContext _myDBContext;
    public SubTarefaRepository(MyDbContext myDbContext){
        _myDBContext = myDbContext;
    }


    public SubTarefa Create(){}
    public SubTarefa Get(int id){}

    public List<SubTarefa> GetAll(){
        return _myDBContext.GetAll();
    }

    public SubTarefa Update(SubTarefa TarefaAtualizada, int id){
        return _myDBContext.Update(TarefaAtualizada, id);
    }

    public void Delete(int id){
        _myDBContext.Delete(id);
    }

}