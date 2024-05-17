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

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Tarefas> Tarefas { get; set; }
    public DbSet<SubTarefas> SubTarefas { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_configuration["ConnectionStrings:AceleraDev"]);
    }
}
