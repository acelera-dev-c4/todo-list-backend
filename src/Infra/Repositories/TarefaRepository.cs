namespace AceleraDevTodoListApi.DB;

public interface ITarefaRepository(){
    Tarefa Create(Tarefa Tarefa);
    Tarefa Get(int id);
    List<Tarefa> GetAll();
    Tarefa Update(Tarefa Tarefa, int id);
    void Delete(int id);
}

public class TarefaRepository : ITarefaRepository{

    private readonly MyDBContext _myDBContext;
    public TarefaRepository(MyDBContext myDBContext){
        _myDBContext = myDBContext;
    }

    public Tarefa Create(Tarefa Tarefa);
    public Tarefa Get(int id);
    public List<Tarefa> GetAll();
    public Tarefa Update(Tarefa Tarefa, int id);
    public void Delete(int id);

}