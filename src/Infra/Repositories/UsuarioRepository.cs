using AceleraDevTodoListApi.DB;
using Domain.Entitys;

namespace AceleraDevTodoListApi.Infra.Repositories;

public interface IUsuarioRepository{
    Usuario Create(Usuario Usuario);
    Usuario Get(int id);
    List<Usuario> GetAll();
    Usuario Update(Usuario Usuario, int id);
    void Delete(int id);
}

public class UsuarioRepository : IUsuarioRepository{

    private readonly MyDBContext _myDBContext;

    public UsuarioRepository(MyDBContext myDBContext){
        _myDBContext = myDBContext;
    }

    public Usuario Create(Usuario Usuario){}
    public Usuario Get(int id){}
    public List<Usuario> GetAll(){}
    public Usuario Update(Usuario Usuario, int id){}
    public void Delete(int id){}

}