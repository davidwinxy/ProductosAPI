using Microsoft.EntityFrameworkCore;
using ProductosAPI.Context;
using ProductosAPI.Models;
using ProductosAPI.Services.Interfaces;

namespace ProductosAPI.Services.Implementaciones
{
    public class ImagenArticuloInterface : IImagenArticuloInterface
    {
        private readonly ProductosContext _context;

        public ImagenArticuloInterface(ProductosContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ImagenArticulo>> GetByArticuloIdAsync(int articuloId)
        {
            return await _context.imagenArticulo
                                 .Where(img => img.ArticuloId == articuloId)
                                 .ToListAsync();
        }

        public async Task<ImagenArticulo> GetByIdAsync(int id)
        {
            return await _context.imagenArticulo.FindAsync(id);
        }

        public async Task AddAsync(ImagenArticulo imagen)
        {
            await _context.imagenArticulo.AddAsync(imagen);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var imagen = await _context.imagenArticulo.FindAsync(id);
            if (imagen != null)
            {
                _context.imagenArticulo.Remove(imagen);
                await _context.SaveChangesAsync();
            }
        }
    }
}
