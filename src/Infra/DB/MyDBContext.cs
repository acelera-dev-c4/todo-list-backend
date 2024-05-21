using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infra.DB;

public class MyDBContext : DbContext
{
    public virtual DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Tarefa> Tarefas { get; set; }
    public virtual DbSet<SubTarefa> SubTarefas { get; set; }

    public MyDBContext(DbContextOptions<MyDBContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tarefa>()
            .HasOne<Usuario>()
            .WithMany()
            .HasForeignKey(t => t.IdUsuario)
            .IsRequired();

        modelBuilder.Entity<SubTarefa>()
            .HasOne<Tarefa>()
            .WithMany()
            .HasForeignKey(st => st.IdTarefa)
            .IsRequired();
    }
}
