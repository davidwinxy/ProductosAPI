﻿using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;
using ProductosAPI.Models;


namespace ProductosAPI.Context
{
    public class ProductosContext : DbContext
    {
        public ProductosContext(DbContextOptions<ProductosContext> options) : base(options) 
        {
                    
        }

        public DbSet<Proveedor> Proveedors { get; set; }
        public DbSet<Compra> compras { get; set; }
        public DbSet<Rol> rol { get; set; }
        public DbSet<UsuarioRol> usuario { get; set; }

    }
}
