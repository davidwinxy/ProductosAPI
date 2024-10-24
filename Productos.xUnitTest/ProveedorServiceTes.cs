using System;
using Microsoft.EntityFrameworkCore;
using ProductosAPI.Context;
using ProductosAPI.Models;
using ProductosAPI.Services.Implementaciones;

namespace Productos.xUnitTest;

public class ProveedorServiceTes
{
        private readonly ProveedorService _proveedorService;
        private readonly ProductosContext _productosContext;

        public ProveedorServiceTes()
        {
            _productosContext = ProductoAPIContextMemory<ProductosContext>.CreateDbContext(Guid.NewGuid().ToString());
            _proveedorService = new ProveedorService(_productosContext);
        }

        [Fact]
        public async Task CreateProveedor()
        {
            var proveedor = new Proveedor
            {
                Nombre = "Juan",
                Apellido = "Perez",
                TipoDePersona = "Natural",
                Dui = "12345678-9",
                NombreEmpresa = "EmpresaTest",
                Nrc = 1234567,
                Contacto = "juan@test.com",
                Telefono = "5555-5555",
                Direccion = "Calle Falsa 123"
            };

            var result = await _proveedorService.CreateProveedor(proveedor);
            var proveedorFromDb = await _productosContext.Proveedors.FirstOrDefaultAsync(p => p.Nombre == "Juan");

            Assert.NotNull(proveedorFromDb);
            Assert.Equal("Juan", proveedorFromDb.Nombre);
            Assert.Equal("Perez", proveedorFromDb.Apellido);
        }

        [Fact]
        public async Task UpdateProveedor()
        {
            var proveedor = new Proveedor
            {
                Nombre = "Maria",
                Apellido = "Lopez",
                TipoDePersona = "Natural",
                Dui = "98765432-1",
                NombreEmpresa = "EmpresaTest",
                Nrc = 7654321,
                Contacto = "maria@test.com",
                Telefono = "6666-6666",
                Direccion = "Calle Real 456"
            };

            _productosContext.Proveedors.Add(proveedor);
            await _productosContext.SaveChangesAsync();

            var updateProveedor = new Proveedor
            {
                Nombre = "Maria Actualizada",
                Apellido = "Lopez",
                TipoDePersona = "Natural",
                Dui = "98765432-1",
                NombreEmpresa = "EmpresaActualizada",
                Nrc = 7654321,
                Contacto = "maria.actualizada@test.com",
                Telefono = "6666-6666",
                Direccion = "Calle Real 456"
            };

            await _proveedorService.UpdateProveedor(updateProveedor, proveedor.Id);

            var proveedorFromDb = await _productosContext.Proveedors.FindAsync(proveedor.Id);
            Assert.NotNull(proveedorFromDb);
            Assert.Equal("Maria Actualizada", proveedorFromDb.Nombre);
            Assert.Equal("EmpresaActualizada", proveedorFromDb.NombreEmpresa);
        }

        [Fact]
        public async Task DeleteProveedor()
        {
            var proveedor = new Proveedor
            {
                Nombre = "Carlos",
                Apellido = "Martinez",
                TipoDePersona = "Juridica",
                Dui = "12312312-3",
                NombreEmpresa = "EmpresaCarlos",
                Nrc = 1122334,
                Contacto = "carlos@test.com",
                Telefono = "7777-7777",
                Direccion = "Calle Nueva 789"
            };

            _productosContext.Proveedors.Add(proveedor);
            await _productosContext.SaveChangesAsync();

            await _proveedorService.DeleteProveedor(proveedor.Id);

            var proveedorFromDb = await _productosContext.Proveedors.FindAsync(proveedor.Id);
            Assert.Null(proveedorFromDb);
        }

        [Fact]
        public async Task GetProveedorById()
        {
            var proveedor = new Proveedor
            {
                Nombre = "Sofia",
                Apellido = "Ramirez",
                TipoDePersona = "Juridica",
                Dui = "45645645-6",
                NombreEmpresa = "EmpresaSofia",
                Nrc = 9988776,
                Contacto = "sofia@test.com",
                Telefono = "8888-8888",
                Direccion = "Calle Principal 123"
            };

            _productosContext.Proveedors.Add(proveedor);
            await _productosContext.SaveChangesAsync();

            var result = await _proveedorService.GetProveedorById(proveedor.Id);

            Assert.NotNull(result);
            Assert.Equal("Sofia", result.Nombre);
            Assert.Equal("EmpresaSofia", result.NombreEmpresa);
        }

        [Fact]
        public async Task GetProveedores()
        {
            var proveedores = new List<Proveedor>
            {
                new Proveedor { Nombre = "Proveedor1", Apellido = "Apellido1", TipoDePersona = "Natural", Dui = "11111111-1", NombreEmpresa = "Empresa1", Nrc = 1111111, Contacto = "proveedor1@test.com", Telefono = "1111-1111", Direccion = "Direccion1" },
                new Proveedor { Nombre = "Proveedor2", Apellido = "Apellido2", TipoDePersona = "Juridica", Dui = "22222222-2", NombreEmpresa = "Empresa2", Nrc = 2222222, Contacto = "proveedor2@test.com", Telefono = "2222-2222", Direccion = "Direccion2" }
            };

            _productosContext.Proveedors.AddRange(proveedores);
            await _productosContext.SaveChangesAsync();

            var result = await _proveedorService.GetProveedors();

            Assert.Equal(2, result.Count());
            Assert.Contains(result, p => p.Nombre == "Proveedor1");
            Assert.Contains(result, p => p.Nombre == "Proveedor2");
        }
}
