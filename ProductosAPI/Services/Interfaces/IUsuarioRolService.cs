using ProductosAPI.Models;

namespace ProductosAPI.Services.Interfaces
{
    public interface IUsuarioRolService
    {
        Task<IEnumerable<UsuarioRol>> GetUsuarioRol();
        Task<UsuarioRol> GetUsuarioRolById(int id);
        Task<UsuarioRol> CreateUsuarioRol(UsuarioRol usuariorol);
        Task UpdateUsuarioRol(UsuarioRol usuariorol, int id);
        Task UsuarioRol(int id);
    }
}
