using AceleraDevTodoListApi.Infra.Repositories;
using Domain.Entitys;
using Domain.Mappers;
using Domain.Request;
using Domain.Responses;

namespace AceleraDevTodoListApi.Services;

public interface IUsuarioService
{
    Usuario Create(UserRequest usuario);
    void Delete(int idUsuario);
    Usuario? GetById(int idUsuario);
    List<UserResponse> List();
    Usuario Update(Usuario usuarioUpdate, int id);
}

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public Usuario Create(UserRequest usuario)
    {
        var newUser = UserMapper.ToEntity(usuario);
        var user = _usuarioRepository.Create(newUser);
        return user;
    }

    public void Delete(int idUsuario)
    {
        _usuarioRepository.Delete(idUsuario);
    }

    public Usuario? GetById(int idUsuario)
    {
        return _usuarioRepository.Get(idUsuario);
    }

    public List<UserResponse> List()
    {
        var users = _usuarioRepository.GetAll();
        var userResponse = users.Select(UserMapper.ToResponse).ToList();
        return userResponse;
    }

    public Usuario Update(Usuario usuarioUpdate, int id)
    {
        return _usuarioRepository.Update(usuarioUpdate, id);
    }
}
