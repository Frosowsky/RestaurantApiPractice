using Microsoft.EntityFrameworkCore;

namespace WebApplication3.Entitie
{
    public class RestaurantDbContext : DbContext
    {
        private string _connectionString = "Data Source=DESKTOP-1NUMHB5\\SQLEXPRESS01;Initial Catalog=Restaurant;Integrated Security=True; Encrypt=false";
        public DbSet<Adress> Adresses { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Dish> Dishes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>()
                .Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(25);
            modelBuilder.Entity<Dish>()
               .Property(d => d.Name)
               .IsRequired();
         
           
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
