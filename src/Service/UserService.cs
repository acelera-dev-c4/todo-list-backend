﻿using Infra.Repositories;
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
    User Update(User userUpdate, int id);
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

    public User Update(User userUpdate, int userId)
    {
        return _userRepository.Update(userUpdate, userId);
    }
}