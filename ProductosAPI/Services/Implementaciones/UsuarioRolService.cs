using Microsoft.EntityFrameworkCore;
using ProductosAPI.Context;
using ProductosAPI.Models;
using ProductosAPI.Services.Interfaces;

namespace ProductosAPI.Services.Implementaciones
{
    public class UsuarioRolService : IUsuarioRolService
    {
        private readonly ProductosContext _context;

        public UsuarioRolService(ProductosContext context)
        {
            _context = context;
        }

        public async Task<UsuarioRol> CreateUsuarioRol(UsuarioRol usuariorol)
        {
            // Validar que el usuario existe
            var usuarioExistente = await _context.usuario.AnyAsync(u => u.Id == usuariorol.IdUsuario);
            if (!usuarioExistente)
            {
                throw new KeyNotFoundException("Usuario no encontrado");
            }

            // Validar que el rol existe
            var rolExistente = await _context.rol.AnyAsync(r => r.Id == usuariorol.IdRol);
            if (!rolExistente)
            {
                throw new KeyNotFoundException("Rol no encontrado");
            }

            // Agregar el usuario-rol al contexto y guardar
            await _context.UsuarioRoles.AddAsync(usuariorol);
            await _context.SaveChangesAsync();

            return usuariorol;
        }

        public async Task<IEnumerable<UsuarioRol>> GetUsuarioRol()
        {
            return await _context.UsuarioRoles.ToListAsync();
        }

        public async Task<UsuarioRol> GetUsuarioRolById(int id)
        {
            var usuarioRol = await _context.UsuarioRoles.FindAsync(id);
            if (usuarioRol == null)
                throw new KeyNotFoundException("UsuarioRol no encontrado");

            return usuarioRol;
        }

        public async Task UpdateUsuarioRol(UsuarioRol usuariorol, int id)
        {
            var usuarioRolExistente = await _context.UsuarioRoles.FirstOrDefaultAsync(u => u.Id == id);
            if (usuarioRolExistente == null)
                throw new KeyNotFoundException("UsuarioRol no encontrado");

            // Actualizar solo los campos que se pueden modificar
            usuarioRolExistente.IdUsuario = usuariorol.IdUsuario; // Asegúrate de que esta propiedad exista
            usuarioRolExistente.IdRol = usuariorol.IdRol; // Asegúrate de que esta propiedad exista

            // Guardar cambios
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUsuarioRol(int id)
        {
            var usuarioRol = await _context.UsuarioRoles.FirstOrDefaultAsync(u => u.Id == id);
            if (usuarioRol == null)
                throw new KeyNotFoundException("UsuarioRol no encontrado");

            _context.UsuarioRoles.Remove(usuarioRol);
            await _context.SaveChangesAsync();
        }

        public Task UsuarioRol(int id)
        {
            throw new NotImplementedException();
        }
    }
}