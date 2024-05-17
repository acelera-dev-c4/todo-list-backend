using AceleraDevTodoListApi.DB;
using Domain.Entitys;
using Microsoft.EntityFrameworkCore;

namespace AceleraDevTodoListApi.Infra.Repositories;

public interface IUsuarioRepository{
    Usuario Create(Usuario Usuario);
    Usuario? Get(int idUsuario);
    List<Usuario> GetAll();
    Usuario Update(Usuario Usuario, int id);
    void Delete(int idUsuario);
}

public class UsuarioRepository : IUsuarioRepository{

    private readonly MyDBContext _myDBContext;

    public UsuarioRepository(MyDBContext myDBContext){
        _myDBContext = myDBContext;
    }

    public Usuario Create(Usuario NovoUsuario){
        _myDBContext.Usuarios.Add(NovoUsuario);
        _myDBContext.SaveChanges();
        return NovoUsuario;
    }
    public Usuario? Get(int idUsuario)
    {
        return _myDBContext.Usuarios.Find(idUsuario);
    }
    public List<Usuario> GetAll()
    {
        return _myDBContext.Usuarios.ToList();
    }
    public Usuario Update(Usuario UsuarioUpdate, int idUsuario)
    {
        if(_myDBContext.Usuarios.Find(idUsuario) is null)
        {
            throw new Exception("Usuário não encontrado para atualização.");
        }

        _myDBContext.Usuarios.Update(UsuarioUpdate);
        _myDBContext.SaveChanges();
        return UsuarioUpdate;
    }
    public void Delete(int idUsuario)
    {
        _myDBContext.Usuarios.Where(x => x.Id == idUsuario).ExecuteDelete();
    }

}