using ProductosAPI.Models;

namespace ProductosAPI.Services.Interfaces
{
    public interface IArticuloInterface
    {
        Task<IEnumerable<Articulo>> GetAllAsync();
        Task<Articulo> GetByIdAsync(int id);
        Task <Articulo> CreateArticulo(Articulo articulo);
        Task UpdateAsync(Articulo articulo, int id);
        Task DeleteAsync(int id);
    }
}
