using Infra.Repositories;
using Domain.Mappers;
using Domain.Request;
using Domain.Responses;
using Domain.Models;

namespace Service;

public interface IUsuarioService
{
    List<UserResponse> List();
    Usuario? GetById(int idUsuario);
    UserResponse Create(UserRequest usuario);
    UserResponse Update(UpdatedUserRequest usuarioUpdate, int idUsuario);
    void Delete(int idUsuario);
}

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public List<UserResponse> List()
    {
        var users = _usuarioRepository.GetAll();
        var userResponse = users.Select(user => UserMapper.ToResponse(user)).ToList();
        return userResponse;
    }

    public Usuario? GetById(int idUsuario)
    {
        return _usuarioRepository.Get(idUsuario);
    }

    public UserResponse Create(UserRequest usuario)
    {
        var newUser = UserMapper.ToEntity(usuario);
        var user = _usuarioRepository.Create(newUser);
        return UserMapper.ToResponse(user);
    }

    public UserResponse Update(UpdatedUserRequest usuarioUpdate, int idUsuario)
    {
        var existingUser = _usuarioRepository.Get(idUsuario);

        if (existingUser is null)
            throw new Exception("Usuário não encontrado!");

        var updatedUser = UserMapper.ToEntity(usuarioUpdate);
        _usuarioRepository.Update(updatedUser, idUsuario);
        return UserMapper.ToResponse(updatedUser);
    }

    public void Delete(int idUsuario)
    {
        _usuarioRepository.Delete(idUsuario);
    }
}
