using Actividad_BibliotecaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Actividad_BibliotecaAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Libro> Libros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) { }
    }
}
