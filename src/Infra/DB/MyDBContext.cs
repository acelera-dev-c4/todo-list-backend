using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infra.DB;

public class MyDBContext : DbContext
{
    public virtual DbSet<Usuario> Usuarios { get; set; }
    public DbSet<MainTask> Tarefas { get; set; }
    public virtual DbSet<SubTarefa> SubTarefas { get; set; }

    public MyDBContext(DbContextOptions<MyDBContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MainTask>()
            .HasOne<Usuario>()
            .WithMany()
            .HasForeignKey(t => t.IdUsuario)
            .IsRequired();

        modelBuilder.Entity<SubTarefa>()
            .HasOne<MainTask>()
            .WithMany()
            .HasForeignKey(st => st.IdTarefa)
            .IsRequired();
    }
}
