using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AceleraDevTodoListApi.DB;

public class MyDBContext : DbContext
{
    private IConfiguration _configuration;
    public MyDBContext(DbContextOptions<MyDBContext> options, IConfiguration configuration) : base(options) 
    {
        _configuration = configuration;
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Quest> Quests { get; set; }
    public DbSet<SubQuest> SubQuests { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_configuration["ConnectionStrings:AceleraDev"]);
    }
}
