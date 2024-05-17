using Domain.Entitys;
using Domain.Request;

namespace Domain.Mappers;

public class UserMapper
{
    public static Usuario ToClass(UserRequest user) => new Usuario
    {
        Id = null,
        Nome = user.Nome,
        Email = user.Email,
        Senha = user.Senha
    };
}
