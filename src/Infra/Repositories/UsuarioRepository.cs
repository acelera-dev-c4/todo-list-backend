using Infra.DB;
using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace Infra.Repositories;

public interface IUsuarioRepository
{
    Usuario Create(Usuario Usuario);
    Usuario? Get(int idUsuario);
    List<Usuario> GetAll();
    Usuario Update(Usuario userUpdate);
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
        var existingUser = Get(userUpdate.Id);

        if (existingUser is null)
            throw new Exception("Usu�rio n�o encontrado para atualiza��o.");

        var teste = _myDBContext.Usuarios.FirstOrDefault(x => x.Id == userUpdate.Id);
        teste.Nome = userUpdate.Nome;
        teste.Senha = userUpdate.Senha;
        teste.Email = userUpdate.Email;
                            
        _myDBContext.SaveChanges();
        return userUpdate;
    }

    public void Delete(int idUsuario)
    {
        _myDBContext.Usuarios.Where(x => x.Id == idUsuario).ExecuteDelete();
    }
}