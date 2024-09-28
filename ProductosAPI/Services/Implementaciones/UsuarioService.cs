using Microsoft.EntityFrameworkCore;
using ProductosAPI.Context;
using ProductosAPI.Models;
using ProductosAPI.Services.Interfaces;

namespace ProductosAPI.Services.Implementaciones
{
    public class UsuarioService : IusuarioService
    {
        private readonly ProductosContext _context;
        public UsuarioService(ProductosContext context)
        {
            _context = context;
        }

        public async Task<usuario> GetUsuariosById(int id)
        {
            return await _context.usuario.FindAsync();
        }

        public async Task<IEnumerable<usuario>> GetUsuarios()
        {
            return await _context.usuario.ToListAsync();
        }
    }
}
