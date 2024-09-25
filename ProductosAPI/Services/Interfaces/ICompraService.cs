using ProductosAPI.Models;

namespace ProductosAPI.Services.Interfaces
{
    public interface ICompraService
    {
        Task<IEnumerable<Compra>> GetCompras();
        Task<Compra> GetCompraById(int id);
        Task<Compra> CreateCompra(Compra compra);
        Task UpdateCompra(Compra compra, int id);
        Task DeleteCompra(int id);
        Task<bool> ExisteProveedor(int idProveedor);
    }
}
