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
            return await _context.usuario.FindAsync(id);
        }

        public async Task<IEnumerable<usuario>> GetUsuarios()
        {
            return await _context.usuario.ToListAsync();
        }

        public async Task<usuario> CreateUsuario(usuario usuario)
        {
            await _context.usuario.AddAsync(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task UpdateUsuario(usuario usuario, int id)
        {

            _context.usuario.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUsuario( int id)
        {
            var usuarios = await _context.usuario.FindAsync(id);
            if (usuarios != null)
            {
                _context.usuario.Remove(usuarios);
                await _context.SaveChangesAsync();
            }
        }
    }
}
