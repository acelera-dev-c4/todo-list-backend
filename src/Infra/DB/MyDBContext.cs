using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infra.DB;

public class MyDBContext : DbContext
{
    public virtual DbSet<User> Users { get; set; }
    public DbSet<MainTask> MainTasks { get; set; }
    public virtual DbSet<SubTask> SubTasks { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<Notifications> Notifications { get; set; }

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

        modelBuilder.Entity<Subscription>()
            .HasOne<MainTask>()
            .WithMany()
            .HasForeignKey(s => s.MainTaskIdTopic)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Subscription>()
            .HasOne<SubTask>()
            .WithMany()
            .HasForeignKey(s => s.SubTaskIdSubscriber)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Notifications>()
            .HasOne<Subscription>()
            .WithMany()
            .HasForeignKey(n => n.SubscriptionId)
            .IsRequired();

    }
}
