using System;
using Microsoft.EntityFrameworkCore;
using ProductosAPI.Context;
using ProductosAPI.Models;
using ProductosAPI.Services.Implementaciones;

namespace Productos.xUnitTest;

public class ImagenArticuloServiceTest
{

    private readonly ImagenArticuloInterface _imagenArticuloService;
    private readonly ProductosContext _productosContext;

    public ImagenArticuloServiceTest()
    {
        _productosContext = ProductoAPIContextMemory<ProductosContext>.CreateDbContext(Guid.NewGuid().ToString());
        _imagenArticuloService = new ImagenArticuloInterface(_productosContext);
    }

    [Fact]
        public async Task AddImagenArticulo()
        {
            var imagen = new ImagenArticulo
            {
                ArticuloId = 1,
                FileName = "testImage.jpg",
                ImageData = new byte[] { 0x01, 0x02 },
                ContentType = "image/jpeg"
            };

            await _imagenArticuloService.AddAsync(imagen);
            var imagenFromDb = await _productosContext.imagenArticulo.FirstOrDefaultAsync(i => i.FileName == "testImage.jpg");

            Assert.NotNull(imagenFromDb);
            Assert.Equal("testImage.jpg", imagenFromDb.FileName);
            Assert.Equal("image/jpeg", imagenFromDb.ContentType);
        }

        [Fact]
        public async Task GetByIdImagenArticulo()
        {
            var imagen = new ImagenArticulo
            {
                ArticuloId = 1,
                FileName = "testImage.jpg",
                ImageData = new byte[] { 0x01, 0x02 },
                ContentType = "image/jpeg"
            };

            await _productosContext.imagenArticulo.AddAsync(imagen);
            await _productosContext.SaveChangesAsync();

            var result = await _imagenArticuloService.GetByIdAsync(imagen.Id);

            Assert.NotNull(result);
            Assert.Equal("testImage.jpg", result.FileName);
            Assert.Equal("image/jpeg", result.ContentType);
        }

        [Fact]
        public async Task DeleteImagenArticulo()
        {
            var imagen = new ImagenArticulo
            {
                ArticuloId = 1,
                FileName = "testImage.jpg",
                ImageData = new byte[] { 0x01, 0x02 },
                ContentType = "image/jpeg"
            };

            await _productosContext.imagenArticulo.AddAsync(imagen);
            await _productosContext.SaveChangesAsync();

            await _imagenArticuloService.DeleteAsync(imagen.Id);

            var imagenFromDb = await _productosContext.imagenArticulo.FindAsync(imagen.Id);
            Assert.Null(imagenFromDb);
        }

        [Fact]
        public async Task GetByArticuloIdImagenArticulo()
        {
            var imagen1 = new ImagenArticulo
            {
                ArticuloId = 1,
                FileName = "testImage1.jpg",
                ImageData = new byte[] { 0x01, 0x02 },
                ContentType = "image/jpeg"
            };

            var imagen2 = new ImagenArticulo
            {
                ArticuloId = 1,
                FileName = "testImage2.jpg",
                ImageData = new byte[] { 0x03, 0x04 },
                ContentType = "image/jpeg"
            };

            await _productosContext.imagenArticulo.AddRangeAsync(new List<ImagenArticulo> { imagen1, imagen2 });
            await _productosContext.SaveChangesAsync();

            var result = await _imagenArticuloService.GetByArticuloIdAsync(1);

            Assert.Equal(2, result.Count());
            Assert.Contains(result, img => img.FileName == "testImage1.jpg");
            Assert.Contains(result, img => img.FileName == "testImage2.jpg");
        }
}
