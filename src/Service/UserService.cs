﻿using Domain.Mappers;
using Domain.Models;
using Domain.Request;
using Domain.Responses;
using Infra.Repositories;

namespace Service;

public interface IUserService
{
    UserResponse Create(UserRequest user);
    void Delete(int userId);
    User? GetById(int userId);
    List<UserResponse> List();
    UserResponse Update(UpdateUserRequest userUpdate);
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

    public UserResponse Create(UserRequest user)
    {
        var newUser = UserMapper.ToEntity(user);
        newUser.Password = _hashingService.Hash(newUser.Password!);
        var createdUser = _userRepository.Create(newUser);
        return UserMapper.ToResponse(createdUser);
    }

    public void Delete(int userId)
    {
        var user = _userRepository.Get(userId);

        if (user is null)
            throw new Exception("User not found!");

        _userRepository.Delete(user);
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

    public UserResponse Update(UpdateUserRequest userUpdate)
    {
        var existingUser = _userRepository.Get(userUpdate.Id);

        if (existingUser is null)
            throw new Exception("User not found!");

        var updatedUser = UserMapper.ToEntity(userUpdate);
        _userRepository.Update(updatedUser);
        return UserMapper.ToResponse(updatedUser);
    }
}
