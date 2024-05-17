using AceleraDevTodoListApi.Infra.Repositories;
using Domain.Entitys;

namespace AceleraDevTodoListApi.Services;

public class UsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public Usuario Create(Usuario usuario)
    {
        return _usuarioRepository.Create(usuario);
    }

    public void Delete(int id)
    {
        _usuarioRepository.Delete(id);
    }

    public Usuario? GetById(int id)
    {
        return _usuarioRepository.Get(id);
    }
    public List<Usuario> List()
    {
        return _usuarioRepository.GetAll();
    }

    public Usuario Update(Usuario usuarioUpdate, int id)
    {
        return _usuarioRepository.Update(usuarioUpdate, id);
    }
}
