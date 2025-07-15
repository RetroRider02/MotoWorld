using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MotoWorld3.Models;

namespace MotoWorld3.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Advertising> Advertisings { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Motorcycle> Motorcycles { get; set; }
    public DbSet<MotorcycleAdvertising> MotorcycleAdvertisings { get; set; }
    public DbSet<MotorcycleType> MotorcycleTypes { get; set; }
    public DbSet<Picture> Pictures { get; set; }
    public DbSet<Place> Places { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MotorcycleAdvertising>()
            .HasKey(ma => new { ma.MotorcycleID, ma.AdvertisingID });

        modelBuilder.Entity<MotorcycleAdvertising>()
            .HasOne(ma => ma.Motorcycle)
            .WithMany(m => m.MotorcycleAdvertisings)
            .HasForeignKey(ma => ma.MotorcycleID);

        modelBuilder.Entity<MotorcycleAdvertising>()
            .HasOne(ma => ma.Advertising)
            .WithMany(a => a.MotorcycleAdvertisings)
            .HasForeignKey(ma => ma.AdvertisingID);

        modelBuilder.Entity<Message>()
            .HasOne(m => m.Advertising)
            .WithMany()
            .HasForeignKey(m => m.AdvertisingID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Message>()
            .HasOne(m => m.IdentityUser)
            .WithMany()
            .HasForeignKey(m => m.IdentityUserID)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
