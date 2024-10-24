using System;
using Microsoft.EntityFrameworkCore;
using ProductosAPI.Context;
using ProductosAPI.Models;
using ProductosAPI.Services.Implementaciones;

namespace Productos.xUnitTest;

public class UsuarioRolServiceTest
{
        private readonly UsuarioRolService _usuarioRolService;
        private readonly ProductosContext _productosContext;

        public UsuarioRolServiceTest()
        {
            _productosContext = ProductoAPIContextMemory<ProductosContext>.CreateDbContext(Guid.NewGuid().ToString());
            _usuarioRolService = new UsuarioRolService(_productosContext);
        }

    [Fact]
    public async Task CreateUsuarioRol()
    {
        var rol = new Rol
        {
            Nombre = "Admin"
        };

        _productosContext.rol.Add(rol);
        await _productosContext.SaveChangesAsync();

        var usuario = new usuario
        {
            Nombre = "Juan",
            Apellido = "Pérez",
            Email = "juan.perez@gmail.com",
            Telefono = "123456789",
            Direccion = "123 Calle Principal"
        };

        _productosContext.usuario.Add(usuario);
        await _productosContext.SaveChangesAsync();

        var usuarioRol = new UsuarioRol
        {
            FechaAsignacion = DateTime.UtcNow,
            IdUsuario = usuario.Id,
            IdRol = rol.Id
        };

        var result = await _usuarioRolService.CreateUsuarioRol(usuarioRol);
        var usuarioRolFromDb = await _productosContext.UsuarioRoles
            .FirstOrDefaultAsync(ur => ur.IdUsuario == usuario.Id && ur.IdRol == rol.Id);

        Assert.NotNull(usuarioRolFromDb);
        Assert.Equal(usuario.Id, usuarioRolFromDb.IdUsuario);
        Assert.Equal(rol.Id, usuarioRolFromDb.IdRol);
    }


        [Fact]
        public async Task UpdateUsuarioRol()
        {
            var usuarioRol = new UsuarioRol
            {
                FechaAsignacion = DateTime.UtcNow,
                IdUsuario = 1,
                IdRol = 1
            };

            _productosContext.UsuarioRoles.Add(usuarioRol);
            await _productosContext.SaveChangesAsync();

            var updateUsuarioRol = new UsuarioRol
            {
                FechaAsignacion = DateTime.UtcNow,
                IdUsuario = 2,
                IdRol = 2
            };

            await _usuarioRolService.UpdateUsuarioRol(updateUsuarioRol, usuarioRol.Id);

            var usuarioRolFromDb = await _productosContext.UsuarioRoles.FindAsync(usuarioRol.Id);
            Assert.NotNull(usuarioRolFromDb);
            Assert.Equal(2, usuarioRolFromDb.IdUsuario);
            Assert.Equal(2, usuarioRolFromDb.IdRol);
        }

        [Fact]
        public async Task DeleteUsuarioRol()
        {
            var usuarioRol = new UsuarioRol
            {
                FechaAsignacion = DateTime.UtcNow,
                IdUsuario = 1,
                IdRol = 1
            };

            _productosContext.UsuarioRoles.Add(usuarioRol);
            await _productosContext.SaveChangesAsync();

            await _usuarioRolService.DeleteUsuarioRol(usuarioRol.Id);

            var usuarioRolFromDb = await _productosContext.UsuarioRoles.FindAsync(usuarioRol.Id);
            Assert.Null(usuarioRolFromDb);
        }

        [Fact]
        public async Task GetUsuarioRolById()
        {
            var usuarioRol = new UsuarioRol
            {
                FechaAsignacion = DateTime.UtcNow,
                IdUsuario = 1,
                IdRol = 1
            };

            _productosContext.UsuarioRoles.Add(usuarioRol);
            await _productosContext.SaveChangesAsync();

            var result = await _usuarioRolService.GetUsuarioRolById(usuarioRol.Id);

            Assert.NotNull(result);
            Assert.Equal(1, result.IdUsuario);
            Assert.Equal(1, result.IdRol);
        }

        [Fact]
        public async Task GetUsuarioRoles()
        {
            var usuarioRoles = new List<UsuarioRol>
            {
                new UsuarioRol { FechaAsignacion = DateTime.UtcNow, IdUsuario = 1, IdRol = 1 },
                new UsuarioRol { FechaAsignacion = DateTime.UtcNow, IdUsuario = 2, IdRol = 2 }
            };

            _productosContext.UsuarioRoles.AddRange(usuarioRoles);
            await _productosContext.SaveChangesAsync();

            var result = await _usuarioRolService.GetUsuarioRol();

            Assert.Equal(2, result.Count());
            Assert.Contains(result, ur => ur.IdUsuario == 1 && ur.IdRol == 1);
            Assert.Contains(result, ur => ur.IdUsuario == 2 && ur.IdRol == 2);
        }
}
