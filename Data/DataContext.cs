using Microsoft.EntityFrameworkCore;
using SFFAPI.Models;

namespace SFFAPI.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<RentedMovie> RentedMovies { get; set; }
        public DbSet<Studio> Studios { get; set; }
        public DbSet<Trivia> Trivias { get; set; }
        public DbSet<User> Users { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DataContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=SFFDatabas.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>()
                        .Property(movie => movie.Title)
                        .IsRequired();

            modelBuilder.Entity<Movie>()
                        .Property(movie => movie.Genre)
                        .IsRequired();

            modelBuilder.Entity<Studio>()
                        .Property(studio => studio.Name)
                        .IsRequired();

            modelBuilder.Entity<Studio>()
                        .Property(studio => studio.City)
                        .IsRequired();
        }
    }
}