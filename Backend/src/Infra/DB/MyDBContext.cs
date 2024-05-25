using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infra.DB;

public class MyDBContext : DbContext
{
    public virtual DbSet<User> Users { get; set; }
    public DbSet<MainTask> MainTasks { get; set; }
    public virtual DbSet<SubTask> SubTasks { get; set; }

    public MyDBContext(DbContextOptions<MyDBContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MainTask>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(t => t.UserId)
            .IsRequired();

        modelBuilder.Entity<SubTask>()
            .HasOne<MainTask>()
            .WithMany()
            .HasForeignKey(st => st.MainTaskId)
            .IsRequired();
    }
}
