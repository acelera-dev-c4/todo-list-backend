using Infra.DB;
using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace Infra.Repositories;

public interface IUserRepository
{
    User Create(User user);
    User? Get(int userId);
    List<User> GetAll();
    User Update(User userUpdate);
    void Delete(User user);
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

    public User Update(User updateUser)
    {
        _myDBContext.Update(updateUser);
        _myDBContext.SaveChanges();
        return updateUser;
    }

    public void Delete(User user)
    {
        _myDBContext.Users.Remove(user);
        _myDBContext.SaveChanges();
    }
}