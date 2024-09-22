using Entidad;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Modelos de la base de datos

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Detalle_Venta> Detalle_Ventas { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Municipio> Municipio { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Slider> Sliders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                    .Property(u => u.FechaRegistro)
                    .HasDefaultValueSql("NOW()");

            modelBuilder.Entity<Usuario>()
           .HasIndex(u => u.Correo)
           .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
