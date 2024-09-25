using ProductosAPI.Models;

namespace ProductosAPI.Services.Interfaces
{
    public interface IProveedorService
    {
        Task<IEnumerable<Proveedor>> GetProveedors();
        Task<Proveedor> GetProveedorById(int id);
        Task<Proveedor> CreateProveedor(Proveedor proveedor);
        Task UpdateProveedor(Proveedor proveedor, int id);
        Task DeleteProveedor(int id);

    }
}
