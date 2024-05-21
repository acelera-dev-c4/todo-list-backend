using Infra.Repositories;
using Domain.Mappers;
using Domain.Request;
using Domain.Responses;
using Domain.Models;

namespace Service;

public interface IUsuarioService
{
    UserResponse Create(UserRequest usuario);
    void Delete(int idUsuario);
    User? GetById(int idUsuario);
    List<UserResponse> List();
    User Update(User usuarioUpdate, int id);
}

public class UsuarioService : IUsuarioService
{
    private readonly IUserRepository _usuarioRepository;

    public UsuarioService(IUserRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public UserResponse Create(UserRequest usuario)
    {
        var newUser = UserMapper.ToEntity(usuario);
        var user = _usuarioRepository.Create(newUser);
        return UserMapper.ToResponse(user);
    }

    public void Delete(int idUsuario)
    {
        _usuarioRepository.Delete(idUsuario);
    }

    public User? GetById(int idUsuario)
    {
        return _usuarioRepository.Get(idUsuario);
    }

    public List<UserResponse> List()
    {
        var users = _usuarioRepository.GetAll();
        var userResponse = users.Select(user => UserMapper.ToResponse(user)).ToList();
        return userResponse;
    }

    public User Update(User usuarioUpdate, int id)
    {
        return _usuarioRepository.Update(usuarioUpdate, id);
    }
}
