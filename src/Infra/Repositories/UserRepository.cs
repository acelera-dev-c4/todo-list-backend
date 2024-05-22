using Infra.DB;
using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace Infra.Repositories;

public interface IUserRepository
{
    User Create(User user);
    User? Get(int userId);
    List<User> GetAll();
    User Update(User userUpdate, int userId);
    void Delete(int userId);
}

public class UserRepository : IUserRepository
{
    private readonly MyDBContext _myDBContext;

    public UserRepository(MyDBContext myDBContext)
    {
        _myDBContext = myDBContext;
    }

    public User Create(User newUser)
    {
        _myDBContext.Users.Add(newUser);
        _myDBContext.SaveChanges();
        return newUser;
    }

    public User? Get(int userId)
    {
        return _myDBContext.Users.Find(userId);
    }

    public List<User> GetAll()
    {
        return _myDBContext.Users.ToList();
    }

    public User Update(User userUpdate, int userId)
    {
        if (_myDBContext.Users.Find(userId) is null)
        {
            throw new Exception("Usuário não encontrado para atualização.");
        }

        _myDBContext.Users.Update(userUpdate);
        _myDBContext.SaveChanges();
        return userUpdate;
    }

    public void Delete(int userId)
    {
        _myDBContext.Users.Where(x => x.Id == userId).ExecuteDelete();
    }
}