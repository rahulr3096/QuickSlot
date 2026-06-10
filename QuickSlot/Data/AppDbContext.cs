using Microsoft.EntityFrameworkCore;
using QuickSlot.Models;

namespace QuickSlot.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();

        public DbSet<Venue> Venues => Set<Venue>();

        public DbSet<Booking> Bookings => Set<Booking>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>()
                .HasIndex(x => new
                {
                    x.VenueId,
                    x.BookingDate,
                    x.SlotTime
                })
                .IsUnique();
        }
    }
}