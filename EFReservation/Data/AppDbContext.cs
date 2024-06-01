using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EFReservation.Models;

namespace EFReservation
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Room>().ToTable("Rooms");
            modelBuilder.Entity<Reservation>().ToTable("Reservations");
            modelBuilder.Entity<Log>().ToTable("Logs");

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Room)
                .WithMany()
                .HasForeignKey(r => r.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Reservation>()
                .Property(r => r.Date)
                .IsRequired();

            modelBuilder.Entity<Room>()
                .Property(r => r.RoomName)
                .IsRequired();

            modelBuilder.Entity<Log>()
                .Property(l => l.Action)
                .IsRequired();

            modelBuilder.Entity<Log>()
                .Property(l => l.Timestamp)
                .IsRequired();
        }
    }
}
