using Domain.Exceptions;
using Domain.Mappers;
using Domain.Models;
using Domain.Request;
using Domain.Responses;
using Infra.Repositories;

namespace Service;

public interface IUserService
{
    Task<UserResponse> Create(UserRequest user);
    Task Delete(int userId);
    Task<User?> GetById(int userId);
    Task<List<UserResponse>> List();
    Task<UserResponse> Update(UserUpdate userUpdate);
    Task<List<User>?> GetByName(string name);

}

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IHashingService _hashingService;

    public UserService(IUserRepository userRepository, IHashingService hashingService)
    {
        _userRepository = userRepository;
        _hashingService = hashingService;
    }

    public async Task<UserResponse> Create(UserRequest user)
    {
        var newUser = UserMapper.ToEntity(user);
        newUser.Password = _hashingService.Hash(newUser.Password!);
        var createdUser = await _userRepository.Create(newUser);
        return UserMapper.ToResponse(createdUser);
    }

    public async Task Delete(int userId)
    {
        await _userRepository.Delete(userId);
    }

    public async Task<User?> GetById(int userId)
    {
        return await _userRepository.Get(userId);
    }

    public async Task<List<User>?> GetByName(string name)
    {
        return await _userRepository.GetByName(name);
    }

    public async Task<List<UserResponse>> List()
    {
        var users = await _userRepository.GetAll();
        var userResponse = users.Select(user => UserMapper.ToResponse(user)).ToList();
        return userResponse;
    }

    public async Task<UserResponse> Update(UserUpdate userUpdate)
    {
        var existingUser = await _userRepository.Get(userUpdate.Id);

        if (existingUser is null)
            throw new NotFoundException("User not found!");

        var updatedUser = UserMapper.ToEntity(userUpdate);
        await _userRepository.Update(updatedUser);
        return UserMapper.ToResponse(updatedUser);
    }
}