using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealEstatePlatform_API.Models;

namespace RealEstatePlatform_API.Data
{
    public class AppDbContext:IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) { }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyImage> PropertiesImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Example: One User (Agent) has many Properties
            modelBuilder.Entity<Property>()
                .HasOne(p => p.Agent)
                .WithMany(u => u.Properties)
                .HasForeignKey(p => p.AgentId)
                .OnDelete(DeleteBehavior.Restrict);

            // One User (Buyer) has many Bookings
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // One User (Buyer) has many Favorites
            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.User)
                .WithMany(u => u.Favorites)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // A Favorite belongs to one Property
            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.Property)
                .WithMany(p => p.Favorites)
                .HasForeignKey(f => f.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);

            // Booking belongs to one Property
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Property)
                .WithMany(p => p.Bookings)
                .HasForeignKey(b => b.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<PropertyImage>()
                .HasOne(pi => pi.Property)
                .WithMany(p => p.Images)
                .HasForeignKey(pi => pi.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);
        }


    }
}
