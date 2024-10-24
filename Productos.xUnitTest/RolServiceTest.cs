using System;
using Microsoft.EntityFrameworkCore;
using ProductosAPI.Context;
using ProductosAPI.Models;
using ProductosAPI.Services.Implementaciones;

namespace Productos.xUnitTest;

public class RolServiceTest
{
        private readonly RolService _rolService;
        private readonly ProductosContext _productosContext;

        public RolServiceTest()
        {
            _productosContext = ProductoAPIContextMemory<ProductosContext>.CreateDbContext(Guid.NewGuid().ToString());
            _rolService = new RolService(_productosContext);
        }

        [Fact]
        public async Task CreateRol()
        {
            var rol = new Rol
            {
                Nombre = "Admin"
            };

            var result = await _rolService.CreateRol(rol);
            var rolFromDb = await _productosContext.rol.FirstOrDefaultAsync(r => r.Nombre == "Admin");

            Assert.NotNull(rolFromDb);
            Assert.Equal("Admin", rolFromDb.Nombre);
        }

        [Fact]
        public async Task UpdateRol()
        {
            var rol = new Rol
            {
                Nombre = "User"
            };

            _productosContext.rol.Add(rol);
            await _productosContext.SaveChangesAsync();

            var updateRol = new Rol
            {
                Nombre = "SuperUser"
            };

            await _rolService.UpdateRol(updateRol, rol.Id);

            var rolFromDb = await _productosContext.rol.FindAsync(rol.Id);
            Assert.NotNull(rolFromDb);
            Assert.Equal("SuperUser", rolFromDb.Nombre);
        }

        [Fact]
        public async Task DeleteRol()
        {
            var rol = new Rol
            {
                Nombre = "Manager"
            };

            _productosContext.rol.Add(rol);
            await _productosContext.SaveChangesAsync();

            await _rolService.DeleteRol(rol.Id);

            var rolFromDb = await _productosContext.rol.FindAsync(rol.Id);
            Assert.Null(rolFromDb);
        }

        [Fact]
        public async Task GetRolById()
        {
            var rol = new Rol
            {
                Nombre = "Guest"
            };

            _productosContext.rol.Add(rol);
            await _productosContext.SaveChangesAsync();

            var result = await _rolService.GetRolById(rol.Id);

            Assert.NotNull(result);
            Assert.Equal("Guest", result.Nombre);
        }

        [Fact]
        public async Task GetRoles()
        {
            var roles = new List<Rol>
            {
                new Rol { Nombre = "Admin" },
                new Rol { Nombre = "User" }
            };

            _productosContext.rol.AddRange(roles);
            await _productosContext.SaveChangesAsync();

            var result = await _rolService.GetRol();

            Assert.Equal(2, result.Count());
            Assert.Contains(result, r => r.Nombre == "Admin");
            Assert.Contains(result, r => r.Nombre == "User");
        }
}
