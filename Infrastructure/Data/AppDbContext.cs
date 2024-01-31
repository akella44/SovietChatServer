using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<InitialSession> InitialSessions { get; set; }
    public DbSet<Session> Sessions { get; set; }
    
    public DbSet<UsersAndChats> UsersAndChats { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chat>()
            .HasMany(c => c.Users)
            .WithMany(u => u.Chats)
            .UsingEntity<UsersAndChats>();
        
        base.OnModelCreating(modelBuilder);
    }
}