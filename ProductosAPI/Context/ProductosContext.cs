using Microsoft.EntityFrameworkCore;
using ProductosAPI.Models;

namespace ProductosAPI.Context
{
    public class ProductosContext : DbContext
    {
        public ProductosContext(DbContextOptions<ProductosContext> options) : base(options)
        {
        }

        public DbSet<Proveedor> Proveedors { get; set; }
        public DbSet<Compra> compras { get; set; } // Cambiado a plural
        public DbSet<Rol> rol { get; set; } // Cambiado a plural
        public DbSet<UsuarioRol> UsuarioRoles { get; set; } // Cambiado a plural
        public DbSet<Login> Login { get; set; }
        // Si necesitas configuraciones adicionales, puedes sobrescribir OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuraciones adicionales pueden ir aquí
            base.OnModelCreating(modelBuilder);
        }
    }
}