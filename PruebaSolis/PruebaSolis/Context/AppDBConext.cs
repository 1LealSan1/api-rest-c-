using Microsoft.EntityFrameworkCore;
using PruebaSolis.Models;

namespace PruebaSolis.Context
{
    public class AppDBContext : DbContext
    {
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Sucursal> Sucursales { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<EmpleadoDetalles> EmpleadosDetalles { get; set; }
        
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder) {}
    }
}
