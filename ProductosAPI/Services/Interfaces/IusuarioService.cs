using ProductosAPI.Models;

namespace ProductosAPI.Services.Interfaces
{
    public interface IusuarioService
    {
        Task<IEnumerable<usuario>> GetUsuarios();
        Task<usuario> GetUsuariosById(int id);

        Task<usuario> CreateUsuario(usuario usuario);
        Task UpdateUsuario(usuario usuario, int id);
        Task DeleteUsuario(int id);

    }
}
