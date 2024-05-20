using Infra.DB;
using Domain.Entitys;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public interface IUsuarioRepository
{
    Usuario Create(Usuario Usuario);
    Usuario? Get(int idUsuario);
    List<Usuario> GetAll();
    Usuario Update(Usuario Usuario);
    void Delete(int idUsuario);
}

public class UsuarioRepository : IUsuarioRepository
{
    private readonly MyDBContext _myDBContext;

    public UsuarioRepository(MyDBContext myDBContext)
    {
        _myDBContext = myDBContext;
    }

    public Usuario Create(Usuario NovoUsuario)
    {
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

    public Usuario Update(Usuario userUpdate)
    {
        var user = Get(userUpdate.Id);

        if (user != null)
            throw new Exception("Usuário não encontrado para atualização.");

        _myDBContext.Usuarios.Update(userUpdate);
        _myDBContext.SaveChanges();
        return userUpdate;
    }

    public void Delete(int idUsuario)
    {
        _myDBContext.Usuarios.Where(x => x.Id == idUsuario).ExecuteDelete();
    }
}