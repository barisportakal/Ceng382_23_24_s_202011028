using Microsoft.EntityFrameworkCore;

namespace EFReservation
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DbSet'ler burada tanımlanır
        // Örnek olarak Reservation ve Room sınıflarını kullanıyoruz
     //   public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Room> Rooms { get; set; }
         public DbSet<Reservation> Reservations { get; set; }  // Yeni eklenen Reservation DbSet


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Veritabanı tablolarının ilişkilerini ve özelliklerini tanımlamak için kullanılır
            // Örneğin, eğer bir ilişki varsa veya özel sütun ayarları yapılması gerekiyorsa burada tanımlanır
        //    modelBuilder.Entity<Reservation>().ToTable("Reservations");
            modelBuilder.Entity<Room>().ToTable("Rooms");
        }
    }
}
