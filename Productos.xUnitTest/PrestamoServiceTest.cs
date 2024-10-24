using Microsoft.EntityFrameworkCore;
using ProductosAPI.Context;
using ProductosAPI.Models;
using ProductosAPI.Services.Implementaciones;
using System;

namespace Productos.xUnitTest;

public class PrestamoServiceTest
{
    private readonly PrestamoService _prestamoService;
    private readonly ProductosContext _productosContext;

    public PrestamoServiceTest() {
        _productosContext = ProductoAPIContextMemory<ProductosContext>.CreateDbContext(Guid.NewGuid().ToString());
        _prestamoService = new PrestamoService(_productosContext);
    }

    [Fact]
    public async Task CreatePrestamo() {
        var articulo = new Articulo {
            Nombre = "Artículo Test",
            Categoria = "Electrónica",
            Descripcion = "Descripción de prueba",
            Disponibilidad = true
        };

        _productosContext.articulo.Add(articulo);
        await _productosContext.SaveChangesAsync();

        var prestamo = new Prestamo {
            Usuario_Id = 1,
            Articulo_Id = articulo.Id,
            Fecha_Prestamo = DateTime.Now,
            Fecha_devolucion = DateTime.Now.AddDays(7)
        };

        var result = await _prestamoService.CreatePrestamo(prestamo);

        Assert.NotNull(result);
        Assert.Equal("En Prestamo", result.Estado);
        Assert.Equal(prestamo.Fecha_devolucion, result.Fecha_devolucion);
    }

    [Fact]
    public async Task UpdatePrestamo() {
        var prestamo = new Prestamo {
            Usuario_Id = 1,
            Articulo_Id = 1,
            Fecha_Prestamo = DateTime.Now,
            Fecha_devolucion = DateTime.Now.AddDays(7)
        };

        _productosContext.Prestamos.Add(prestamo);
        await _productosContext.SaveChangesAsync();

        _productosContext.Entry(prestamo).State = EntityState.Detached;

        var updatePrestamo = new Prestamo {
            Id = prestamo.Id,
            Usuario_Id = 1,
            Articulo_Id = 1,
            Fecha_Prestamo = prestamo.Fecha_Prestamo,
            Fecha_devolucion = DateTime.Now.AddDays(10)
        };

        await _prestamoService.UpdatePrestamo(updatePrestamo);

        var prestamoFromDb = await _productosContext.Prestamos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == prestamo.Id);
        Assert.NotNull(prestamoFromDb);
        Assert.Equal(updatePrestamo.Fecha_devolucion, prestamoFromDb.Fecha_devolucion);
        Assert.Equal("En Prestamo", prestamoFromDb.Estado);
    }

    [Fact]
    public async Task DeletePrestamo() {
        var prestamo = new Prestamo {
            Usuario_Id = 1,
            Articulo_Id = 1,
            Fecha_Prestamo = DateTime.Now,
            Fecha_devolucion = DateTime.Now.AddDays(7)
        };

        _productosContext.Prestamos.Add(prestamo);
        await _productosContext.SaveChangesAsync();

        await _prestamoService.DeletePrestamo(prestamo.Id);

        var prestamoFromDb = await _productosContext.Prestamos.FindAsync(prestamo.Id);
        Assert.Null(prestamoFromDb);
    }

    [Fact]
    public async Task GetPrestamoById() {
        var prestamo = new Prestamo {
            Usuario_Id = 1,
            Articulo_Id = 1,
            Fecha_Prestamo = DateTime.Now,
            Fecha_devolucion = DateTime.Now.AddDays(7)
        };

        _productosContext.Prestamos.Add(prestamo);
        await _productosContext.SaveChangesAsync();

        var result = await _prestamoService.GetPrestamoById(prestamo.Id);

        Assert.NotNull(result);
        Assert.Equal(1, result.Usuario_Id);
        Assert.Equal(1, result.Articulo_Id);
        Assert.Equal("En Prestamo", result.Estado);
    }

    [Fact]
    public async Task GetPrestamos() {
        var prestamos = new List<Prestamo> {
            new Prestamo { Usuario_Id = 1, Articulo_Id = 1, Fecha_Prestamo = DateTime.Now, Fecha_devolucion = DateTime.Now.AddDays(7) },
            new Prestamo { Usuario_Id = 2, Articulo_Id = 2, Fecha_Prestamo = DateTime.Now, Fecha_devolucion = DateTime.Now.AddDays(7) }
        };

        _productosContext.Prestamos.AddRange(prestamos);
        await _productosContext.SaveChangesAsync();

        var result = await _prestamoService.GetAllPrestamos();

        Assert.Equal(2, result.Count());
        Assert.Contains(result, p => p.Usuario_Id == 1);
        Assert.Contains(result, p => p.Usuario_Id == 2);
    }
}
