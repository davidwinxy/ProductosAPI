using ProductosAPI.Models;

namespace ProductosAPI.Services.Interfaces
{
    public interface IRolService
    {
        Task<IEnumerable<Rol>> GetRol();
        Task<Rol> GetRolById(int id);
        Task<Rol> CreateRol(Rol rol);
        Task UpdateRol(Rol rol, int id);
        Task DeleteRol(int id);
    }
}
