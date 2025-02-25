using AppBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace AppBackend.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<ProductoSinDescripcionDto> ProductoSinDescripcionDto { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }
        public object Producto { get; internal set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configura ProductoSinDescripcionDto como una entidad sin clave
            modelBuilder.Entity<ProductoSinDescripcionDto>().HasNoKey().ToView(null);

            // Resto de la configuración...
        }

    }
}  