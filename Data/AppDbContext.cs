using Microsoft.EntityFrameworkCore;
using lab_1_POE_U20200218.Models;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace lab_1_POE_U20200218.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Empleado> Empleados { get; set; }

        private readonly string _connectionString;

        public AppDbContext()
        {
            // Intentar leer la cadena de conexión desde appsettings.json
            try
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);

                var config = builder.Build();
                var conn = config.GetConnectionString("DefaultConnection");
                if (!string.IsNullOrWhiteSpace(conn))
                {
                    _connectionString = conn;
                }
                else
                {
                    // Fallback si no existe la configuración
                    _connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=FichaEmpleadoDb;Trusted_Connection=True;MultipleActiveResultSets=true";
                }
            }
            catch
            {
                _connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=FichaEmpleadoDb;Trusted_Connection=True;MultipleActiveResultSets=true";
            }
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Apellido).HasMaxLength(100).IsRequired();
                entity.Property(e => e.DUI).HasMaxLength(12);
                entity.Property(e => e.Cargo).HasMaxLength(100);
            });
        }
    }
}
