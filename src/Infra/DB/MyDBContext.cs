using Domain.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AceleraDevTodoListApi.Infra.DB;

public class MyDBContext : DbContext
{
    private IConfiguration _configuration;
    public MyDBContext() { }
    public MyDBContext(DbContextOptions<MyDBContext> options, IConfiguration configuration) : base(options) 
    {
        _configuration = configuration;
    }

    public virtual DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Tarefa> Tarefas { get; set; }
    public DbSet<SubTarefa> SubTarefas { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_configuration["ConnectionStrings:AceleraDev"]);
    }
}
