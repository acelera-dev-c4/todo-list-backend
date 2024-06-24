using Domain.Exceptions;
using Domain.Models;
using Infra.DB;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public interface IUserRepository
{
    Task<User> Create(User user);
    Task<User?> Get(int userId);
    Task<List<User>> GetAll();
    Task<User> Update(User userUpdate);
    Task Delete(int userId);
    Task<User?> FindByUsernameAsync(string username);
    Task<List<User>> GetByName(string name);
}

public class UserRepository : IUserRepository
{
    private readonly MyDBContext _myDBContext;

    public UserRepository(MyDBContext myDBContext)
    {
        _myDBContext = myDBContext;
    }

    public async Task<User?> FindByUsernameAsync(string username)
    {
        return await _myDBContext.Users.FirstOrDefaultAsync(u => u.Email == username);
    }

    public async Task<User> Create(User newUser)
    {
        await _myDBContext.Users.AddAsync(newUser);
        await _myDBContext.SaveChangesAsync();
        return newUser;
    }

    public async Task<User?> Get(int userId)
    {
        return await _myDBContext.Users.FindAsync(userId);
    }

    public async Task<List<User>> GetAll()
    {
        return await _myDBContext.Users.ToListAsync();
    }

    public async Task<User> Update(User userUpdate)
    {
        var updatedUser = await _myDBContext.Users.
            FirstOrDefaultAsync(x => x.Id == userUpdate.Id) ?? throw new NotFoundException("User not found!");

        updatedUser.Name = userUpdate.Name;
        updatedUser.Password = userUpdate.Password;
        updatedUser.Email = userUpdate.Email;

        await _myDBContext.SaveChangesAsync();
        return userUpdate;
    }

    public async Task Delete(int userId)
    {
        var user = await Get(userId);

        if (user is null)
            throw new NotFoundException("User not found!");

        await _myDBContext.Users.Where(x => x.Id == userId).ExecuteDeleteAsync();
        await _myDBContext.SaveChangesAsync();
    }

    public async Task<List<User>> GetByName(string name)
    {
        return await _myDBContext.Users.Where(x => x.Name!.Contains(name)).ToListAsync();
    }
}