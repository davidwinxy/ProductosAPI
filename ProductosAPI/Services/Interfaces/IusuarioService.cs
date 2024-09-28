using ProductosAPI.Models;

namespace ProductosAPI.Services.Interfaces
{
    public interface IusuarioService
    {
        Task<IEnumerable<usuario>> GetUsuarios();
        Task<usuario> GetUsuariosById(int id);

    }
}
