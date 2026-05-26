using Microsoft.EntityFrameworkCore;
using WebApplication25.Entity;

namespace WebApplication25.db;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }


    public DbSet<User> Users { get; set; }
    public DbSet<UserToken> UserTokens { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.HasIndex(u => u.Email).IsUnique();
            entity.Property(u => u.Email).IsRequired().HasMaxLength(150);
            entity.Property(u => u.PasswordHash).IsRequired();
            entity.Property(u => u.Role).HasMaxLength(20);
        });


        modelBuilder.Entity<UserToken>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.TokenValue).IsRequired();
            entity.Property(t => t.TokenType).IsRequired().HasMaxLength(50);
            
            entity.HasOne(t => t.User)
                .WithMany(u => u.Tokens)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
    
}