using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<InitialSession> Sessions { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chat>()
            .HasMany(e => e.Users)
            .WithMany(e => e.Chats)
            .UsingEntity<UsersAndChats>();
        
        base.OnModelCreating(modelBuilder);
    }
}