using Infra.Repositories;
using Domain.Mappers;
using Domain.Request;
using Domain.Responses;
using Domain.Models;

namespace Service;

public interface IUserService
{
    UserResponse Create(UserRequest user);
    void Delete(int userId);
    User? GetById(int userId);
    List<UserResponse> List();
    User Update(User updatedUser, int userId);
}

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public UserResponse Create(UserRequest user)
    {
        var newUser = UserMapper.ToEntity(user);
        var user = _userRepository.Create(newUser);
        return UserMapper.ToResponse(user);
    }

    public void Delete(int userId)
    {
        _userRepository.Delete(userId);
    }

    public User? GetById(int userId)
    {
        return _userRepository.Get(userId);
    }

    public List<UserResponse> List()
    {
        var users = _userRepository.GetAll();
        var userResponse = users.Select(user => UserMapper.ToResponse(user)).ToList();
        return userResponse;
    }

    public User Update(User updatedUser, int userId)
    {
        return _userRepository.Update(updatedUser, userId);
    }
}
