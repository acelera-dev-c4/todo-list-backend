using Domain.Entitys;
using Microsoft.EntityFrameworkCore;

namespace AceleraDevTodoListApi.DB;

public class MyDBContext : DbContext
{
    public MyDBContext(DbContextOptions<MyDBContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Tarefa> Tarefas { get; set; }
    public DbSet<SubTarefa> SubTarefas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(DatabaseHelper.ConnectionString);
    }
}
