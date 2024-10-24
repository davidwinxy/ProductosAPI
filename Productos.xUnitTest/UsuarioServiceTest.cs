using ProductosAPI.Context;
using ProductosAPI.Models;
using ProductosAPI.Services.Implementaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productos.xUnitTest
{
    public class UsuarioServiceTest
    {
        private readonly UsuarioService _usuarioService;
        private readonly ProductosContext _productoAPIContext;

        public UsuarioServiceTest()
        {
            _productoAPIContext = ProductoAPIContextMemory<ProductosContext>.CreateDbContext(Guid.NewGuid().ToString());
            _usuarioService = new UsuarioService(_productoAPIContext);
        }

        [Fact]
        public async Task GetUsuarioById_ReturnsUsuario()
        {
            var usuario = new usuario
            {
                Nombre = "Juan",
                Apellido = "Pérez",
                Email = "juan.perez@gmail.com",
                Telefono = "123456789",
                Direccion = "123 Calle Principal"
            };

            _productoAPIContext.usuario.Add(usuario);
            await _productoAPIContext.SaveChangesAsync();

            var result = await _usuarioService.GetUsuariosById(usuario.Id);

            Assert.NotNull(result);
            Assert.Equal("Juan", result.Nombre);
            Assert.Equal("Pérez", result.Apellido);
        }

        [Fact]
        public async Task GetUsuarios_ReturnsAllUsuarios()
        {
            var usuarios = new List<usuario>
            {
                new usuario { Nombre = "Ana", Apellido = "Rodriguez", Email = "ana.rodriguez@gmail.com", Telefono = "555666777", Direccion = "654 Calle Sexta" },
                new usuario { Nombre = "Mario", Apellido = "Fernandez", Email = "mario.fernandez@gmail.com", Telefono = "333444555", Direccion = "321 Calle Septima" }
            };

            _productoAPIContext.usuario.AddRange(usuarios);
            await _productoAPIContext.SaveChangesAsync();

            var result = await _usuarioService.GetUsuarios();

            Assert.Equal(2, result.Count());
            Assert.Contains(result, u => u.Nombre == "Ana");
            Assert.Contains(result, u => u.Nombre == "Mario");
        }
    }
}

