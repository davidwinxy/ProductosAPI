using Microsoft.EntityFrameworkCore;
using ProductosAPI.Context;
using ProductosAPI.Models;
using ProductosAPI.Services.Interfaces;


namespace ProductosAPI.Services.Implementaciones
{
    public class RolService : IRolService
    {

        private readonly ProductosContext _context;
        public RolService(ProductosContext context)
        {
            _context = context;
        }

        public async Task<Rol> CreateRol(Rol rol)
        {
            _context.rol.Add(rol);
            await _context.SaveChangesAsync();
            return rol;
        }

        public async Task DeleteRol(int id)
        {
            var rol = await _context.rol.FirstOrDefaultAsync(r => r.Id == id);
            if (rol == null) throw new KeyNotFoundException("rol no encontrado");
            {
                _context.rol.Remove(rol);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Rol> GetRolById(int id)
        {
            return await _context.rol.FindAsync(id);
        }

        public async Task<IEnumerable<Rol>> GetRol()
        {
            return await _context.rol.ToListAsync();
        }

        public async Task UpdateRol(Rol rol, int id)
        {
            var rolExistente = await _context.rol.FirstOrDefaultAsync(r => r.Id == id);
            if (rolExistente == null) throw new KeyNotFoundException("rol no encontrado");
            rolExistente.Nombre = rol.Nombre;
            await _context.SaveChangesAsync();
        }

        
    }

       
    }

