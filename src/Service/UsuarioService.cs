using Infra.Repositories;
using Domain.Entitys;
using Domain.Mappers;
using Domain.Request;
using Domain.Responses;

namespace Services;

public interface IUsuarioService
{
    List<UserResponse> List();
    Usuario? GetById(int idUsuario);
    UserResponse Create(UserRequest usuario);
    UserResponse Update(UpdatedUserRequest usuarioUpdate);
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

    public UserResponse Update(UpdatedUserRequest usuarioUpdate)
    {
        var existingUser = _usuarioRepository.Get(usuarioUpdate.Id);

        if (existingUser is null)
            throw new Exception("Usuário não encontrado!");

        var updatedUser = UserMapper.ToEntity(usuarioUpdate);
        var user = _usuarioRepository.Update(updatedUser);
        return UserMapper.ToResponse(user);
    }

    public void Delete(int idUsuario)
    {
        _usuarioRepository.Delete(idUsuario);
    }
}
