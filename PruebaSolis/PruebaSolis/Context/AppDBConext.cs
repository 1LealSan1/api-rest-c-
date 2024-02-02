using Microsoft.EntityFrameworkCore;
using PruebaSolis.Models;

namespace PruebaSolis.Context
{
    public class AppDBContext : DbContext
    {
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Sucursal> Sucursal { get; set; }
        public DbSet<Empleado> Empleado { get; set; }

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
