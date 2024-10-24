using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductosAPI.Context;
using ProductosAPI.Models;
using ProductosAPI.Services.Implementaciones;
using Xunit;

namespace Productos.xUnitTest
{
    public class ArticuloServiceTest
    {
        private readonly ArticuloService _articuloService;
        private readonly ProductosContext _productoAPIContext;

        public ArticuloServiceTest()
        {
            _productoAPIContext = ProductoAPIContextMemory<ProductosContext>.CreateDbContext(Guid.NewGuid().ToString());
            _articuloService = new ArticuloService(_productoAPIContext);
        }

        [Fact]
        public async Task CreateArticulo()
        {
            var articulo = new Articulo
            {
                Nombre = "testArticulo",
                Descripcion = "Descripcion del articulo",
                Categoria = "Electronica",
                Disponibilidad = true
            };

            var result = await _articuloService.CreateArticulo(articulo);
            var articuloFromDb = await _productoAPIContext.articulo.FirstOrDefaultAsync(a => a.Nombre == "testArticulo");

            Assert.NotNull(articuloFromDb);
            Assert.Equal("testArticulo", articuloFromDb.Nombre);
            Assert.Equal("Descripcion del articulo", articuloFromDb.Descripcion);
            Assert.Equal("Electronica", articuloFromDb.Categoria);
        }

       [Fact]
public async Task UpdateArticulo()
{
    var articulo = new Articulo
    {
        Nombre = "oldArticulo",
        Descripcion = "Descripcion antigua",
        Categoria = "Electrodomesticos",
        Disponibilidad = true
    };

    // Agregar el artículo inicial a la base de datos
    _productoAPIContext.articulo.Add(articulo);
    await _productoAPIContext.SaveChangesAsync();

    // Actualizar el artículo existente en la base de datos
    await _articuloService.UpdateAsync(new Articulo
    {
        Id = articulo.Id, // Asegurarse de incluir el ID correcto
        Nombre = "newArticulo",
        Descripcion = "Nueva descripcion",
        Categoria = "Electrodomesticos", // Verificar que no cambia si no se actualiza
        Disponibilidad = false
    }, articulo.Id); // Aquí se pasa el ID del artículo

    // Verificar que el artículo se haya actualizado correctamente
    var articuloFromDb = await _productoAPIContext.articulo.FindAsync(articulo.Id);
    
    Assert.NotNull(articuloFromDb);
    Assert.Equal("newArticulo", articuloFromDb.Nombre);
    Assert.Equal("Nueva descripcion", articuloFromDb.Descripcion);
    Assert.Equal("Electrodomesticos", articuloFromDb.Categoria); // Verificar que la categoría no haya cambiado
}

        [Fact]
        public async Task DeleteArticulo()
        {
            var articulo = new Articulo
            {
                Nombre = "testArticulo",
                Descripcion = "Descripcion del articulo",
                Categoria = "Electrodomesticos",
                Disponibilidad = true
            };

            // Agregar el artículo inicial a la base de datos
            _productoAPIContext.articulo.Add(articulo);
            await _productoAPIContext.SaveChangesAsync();

            // Eliminar el artículo de la base de datos
            await _articuloService.DeleteAsync(articulo.Id);

            // Verificar que el artículo ya no exista en la base de datos
            var articuloFromDb = await _productoAPIContext.articulo.FindAsync(articulo.Id);
            Assert.Null(articuloFromDb);
        }

        [Fact]
        public async Task GetByArticuloIdAsync()
        {
            var articulo = new Articulo
            {
                Nombre = "testArticulo",
                Descripcion = "Descripcion del articulo",
                Categoria = "Electrodomesticos",
                Disponibilidad = true,
                Imagenes = new List<ImagenArticulo>
                {
                    new ImagenArticulo
                    {
                        FileName = "imagen1.jpg",
                        ImageData = new byte[] { 0x20, 0x20 },
                        ContentType = "image/jpeg"
                    }
                }
            };

            // Agregar el artículo inicial a la base de datos
            _productoAPIContext.articulo.Add(articulo);
            await _productoAPIContext.SaveChangesAsync();

            // Obtener el artículo por ID desde el servicio
            var result = await _articuloService.GetByIdAsync(articulo.Id);

            // Verificar que se haya obtenido el artículo correctamente
            Assert.NotNull(result);
            Assert.Equal("testArticulo", result.Nombre);
            Assert.Single(result.Imagenes); // Verificar que hay una sola imagen
            Assert.Contains(result.Imagenes, i => i.FileName == "imagen1.jpg");
        }

        [Fact]
        public async Task GetArticulos()
        {
            var articulos = new List<Articulo>
            {
                new Articulo { Nombre = "Articulo1", Descripcion = "Descripcion1", Categoria = "Categoria1", Disponibilidad = true },
                new Articulo { Nombre = "Articulo2", Descripcion = "Descripcion2", Categoria = "Categoria2", Disponibilidad = false }
            };

            // Agregar los artículos iniciales a la base de datos
            _productoAPIContext.articulo.AddRange(articulos);
            await _productoAPIContext.SaveChangesAsync();

            // Obtener todos los artículos desde el servicio
            var result = await _articuloService.GetAllAsync();

            // Verificar que se hayan obtenido los artículos correctamente
            Assert.Equal(2, result.Count());
            Assert.Contains(result, a => a.Nombre == "Articulo1");
            Assert.Contains(result, a => a.Nombre == "Articulo2");
        }
    }
}
