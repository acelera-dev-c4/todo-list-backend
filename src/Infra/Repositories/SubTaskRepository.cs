namespace AceleraDevTodoListApi.DB;

public interface ISubTaskRepository(){
    SubTask Create(SubTask newSubTask);
    SubTask Get(int mainTaskId);
    List<SubTask> GetAll();
    SubTask Update(SubTask updatedSubTask, int subTaskId);
    void Delete(int subTaskId);
    }


public class SubTaskRepository : ISubTaskRepository{

    private readonly MyDBContext _myDBContext;
    public SubTaskRepository(MyDbContext myDbContext){
        _myDBContext = myDbContext;
    }


    public SubTask Create(SubTask newSubTask){
        _myDBContext.save(newSubTask)
    }
    public SubTask Get(int mainTaskId){
        return _myDBContext.Find(mainTaskId);
    }

    public List<SubTask> GetAll(){
        return _myDBContext.SubTasks.FindAll();
    }

    public SubTask Update(SubTask updatedSubTask, int subtaskId){
        return _myDBContext.Update(updatedSubTask, subtaskId);
    }

    public void Delete(int subtaskId){
        _myDBContext.Delete(subtaskId);
    }

}