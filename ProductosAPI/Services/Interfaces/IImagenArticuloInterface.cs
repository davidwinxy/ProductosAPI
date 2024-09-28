using ProductosAPI.Models;

namespace ProductosAPI.Services.Interfaces
{
    public interface IImagenArticuloInterface
    {
        Task<IEnumerable<ImagenArticulo>> GetByArticuloIdAsync(int articuloId);
        Task<ImagenArticulo> GetByIdAsync(int id);
        Task AddAsync(ImagenArticulo imagen);
        Task DeleteAsync(int id);
    }
}
