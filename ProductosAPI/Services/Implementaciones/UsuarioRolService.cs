using ProductosAPI.Context;
using ProductosAPI.Models;
using ProductosAPI.Services.Interfaces;

namespace ProductosAPI.Services.Implementaciones
{
    public class UsuarioRolService : IUsuarioRolService
    {
        private readonly UsuarioRolContext _context;
        public UsuarioRolService(UsuarioRolContext context)
        {
            _context = context;
        }

        public Task<UsuarioRol> CreateUsuarioRol(UsuarioRol usuariorol)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UsuarioRol>> GetUsuarioRol()
        {
            throw new NotImplementedException();
        }

        public Task<UsuarioRol> GetUsuarioRolById(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUsuarioRol(UsuarioRol usuariorol, int id)
        {
            throw new NotImplementedException();
        }

        public Task UsuarioRol(int id)
        {
            throw new NotImplementedException();
        }
    }
}
