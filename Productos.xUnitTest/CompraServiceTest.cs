using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductosAPI.Context;
using ProductosAPI.Models;
using ProductosAPI.Services.Implementaciones;
using Xunit;

namespace Productos.xUnitTest;

public class CompraServiceTest
{
    private readonly CompraService _compraService;
    private readonly ProductosContext _productosContext;

    public CompraServiceTest()
    {
        _productosContext = ProductoAPIContextMemory<ProductosContext>.CreateDbContext(Guid.NewGuid().ToString());
        _compraService = new CompraService(_productosContext);
    }

    [Fact]
    public async Task CreateCompra()
    {
        // Asegúrate de que existe un proveedor con todas las propiedades necesarias
        var proveedor = new Proveedor
        {
            Id = 1,
            Nombre = "Proveedor Test",
            Apellido = "Apellido Test",
            Contacto = "123456789",
            Direccion = "Direccion Test",
            Dui = "12345678-9",
            NombreEmpresa = "Empresa Test",
            Nrc = 123456,
            Telefono = "987654321",
            TipoDePersona = "Natural"
        };

        _productosContext.Proveedors.Add(proveedor);
        await _productosContext.SaveChangesAsync();

        var compra = new Compra
        {
            NumerodeFactura = "12345",
            Fecha = DateTime.Now,
            PrecioUnitario = 100.00m,
            Cantidad = 2,
            IdProveedor = proveedor.Id // Usa el ID del proveedor que acabas de agregar
        };

        // Llama al método para calcular los totales antes de guardar
        compra.CalcularTotales();

        var result = await _compraService.CreateCompra(compra);
        var compraFromDb = await _productosContext.compras.FirstOrDefaultAsync(c => c.NumerodeFactura == "12345");

        Assert.NotNull(compraFromDb);
        Assert.Equal("12345", compraFromDb.NumerodeFactura);
        Assert.Equal(200.00m, compraFromDb.SubTotal);
        Assert.Equal(26.00m, compraFromDb.Iva); // 13% de 200.00
        Assert.Equal(226.00m, compraFromDb.Total);
    }

    [Fact]
    public async Task UpdateCompra()
    {
        // Similar a CreateCompra, asegúrate de que existe un proveedor
        var proveedor = new Proveedor
        {
            Id = 1,
            Nombre = "Proveedor Test",
            Apellido = "Apellido Test",
            Contacto = "123456789",
            Direccion = "Direccion Test",
            Dui = "12345678-9",
            NombreEmpresa = "Empresa Test",
            Nrc = 123456,
            Telefono = "987654321",
            TipoDePersona = "Natural"
        };

        _productosContext.Proveedors.Add(proveedor);
        await _productosContext.SaveChangesAsync();

        var compra = new Compra
        {
            NumerodeFactura = "12345",
            Fecha = DateTime.Now,
            PrecioUnitario = 100.00m,
            Cantidad = 2,
            IdProveedor = proveedor.Id
        };

        // Llama al método para calcular los totales antes de guardar
        compra.CalcularTotales();
        _productosContext.compras.Add(compra);
        await _productosContext.SaveChangesAsync();

        var updateCompra = new Compra
        {
            NumerodeFactura = "12345",
            PrecioUnitario = 150.00m,
            Cantidad = 3,
            IdProveedor = proveedor.Id
        };

        // Calcula los totales para la compra actualizada
        updateCompra.CalcularTotales();
        await _compraService.UpdateCompra(updateCompra, compra.Id);

        var compraFromDb = await _productosContext.compras.FindAsync(compra.Id);
        Assert.NotNull(compraFromDb);
        Assert.Equal("12345", compraFromDb.NumerodeFactura);
        Assert.Equal(450.00m, compraFromDb.SubTotal); // 150.00 * 3
        Assert.Equal(58.50m, compraFromDb.Iva); // 13% de 450.00
        Assert.Equal(508.50m, compraFromDb.Total);
    }

    [Fact]
    public async Task DeleteCompra()
    {
        var compra = new Compra
        {
            NumerodeFactura = "12345",
            Fecha = DateTime.Now,
            PrecioUnitario = 100.00m,
            Cantidad = 2,
            IdProveedor = 1
        };

        compra.CalcularTotales(); // Asegúrate de calcular los totales
        _productosContext.compras.Add(compra);
        await _productosContext.SaveChangesAsync();

        await _compraService.DeleteCompra(compra.Id);

        var compraFromDb = await _productosContext.compras.FindAsync(compra.Id);
        Assert.Null(compraFromDb);
    }

    [Fact]
    public async Task GetCompraById()
    {
        var proveedor = new Proveedor
        {
            Id = 1,
            Nombre = "Proveedor Test",
            Apellido = "Apellido Test",
            Contacto = "123456789",
            Direccion = "Direccion Test",
            Dui = "12345678-9",
            NombreEmpresa = "Empresa Test",
            Nrc = 123456,
            Telefono = "987654321",
            TipoDePersona = "Natural"
        };

        _productosContext.Proveedors.Add(proveedor);
        await _productosContext.SaveChangesAsync();

        var compra = new Compra
        {
            NumerodeFactura = "12345",
            Fecha = DateTime.Now,
            PrecioUnitario = 100.00m,
            Cantidad = 2,
            IdProveedor = proveedor.Id
        };

        compra.CalcularTotales(); 
        _productosContext.compras.Add(compra);
        await _productosContext.SaveChangesAsync();

        var result = await _compraService.GetCompraById(compra.Id);

        Assert.NotNull(result);
        Assert.Equal("12345", result.NumerodeFactura);
        Assert.Equal(200.00m, result.SubTotal);
        Assert.Equal(26.00m, result.Iva);
        Assert.Equal(226.00m, result.Total);
    }

    [Fact]
    public async Task GetCompras()
    {
        var compras = new List<Compra>
        {
            new Compra { NumerodeFactura = "Factura1", Fecha = DateTime.Now, PrecioUnitario = 50.00m, Cantidad = 1, IdProveedor = 1 },
            new Compra { NumerodeFactura = "Factura2", Fecha = DateTime.Now, PrecioUnitario = 75.00m, Cantidad = 2, IdProveedor = 1 }
        };

        foreach (var compra in compras)
        {
            compra.CalcularTotales();
        }

        _productosContext.compras.AddRange(compras);
        await _productosContext.SaveChangesAsync();

        var result = await _compraService.GetCompras();

        Assert.Equal(2, result.Count());
        Assert.Contains(result, c => c.NumerodeFactura == "Factura1");
        Assert.Contains(result, c => c.NumerodeFactura == "Factura2");
    }
}