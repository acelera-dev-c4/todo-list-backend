﻿using Domain.Entitys;
using Domain.Request;
using Domain.Responses;

namespace Domain.Mappers;

public class UserMapper
{
    public static UserResponse ToResponse(Usuario user) => new()
    {
        Id = user.Id,
        Name = user.Nome,
        Email = user.Email,
    };
    public static Usuario ToEntity(UserRequest user) => new()
    {
        Nome = user.Nome,
        Email = user.Email,
        Senha = user.Senha
    };
    public static Usuario ToEntity(UpdatedUserRequest user) => new()
    {
        Id = user.Id,
        Nome = user.Nome,
        Email = user.Email,
        Senha = user.Senha
    };
}
