using Domain.Models;
using Domain.Request;
using Domain.Responses;

namespace Domain.Mappers;

public class UserMapper
{
    public static UserResponse ToResponse(User user) => new()
    {
        Id = user.Id,
        Name = user.Name,
        Email = user.Email,
    };
    public static User ToEntity(UserRequest user) => new()
    {
        Id = null,
        Name = user.Name,
        Email = user.Email,
        Password = user.Password
    };
    public static User ToEntity(UpdatedUserRequest user) => new()
    {
        Id = user.Id,
        Name = user.Name,
        Email = user.Email
    };
}
