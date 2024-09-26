using Microsoft.EntityFrameworkCore;
using ProductosAPI.Context;
using ProductosAPI.Models;
using ProductosAPI.Services.Interfaces;

namespace ProductosAPI.Services.Implementaciones
{
    public class ProveedorService : IProveedorService
    {
        private readonly RolContext _context;
        public ProveedorService(RolContext context)
        {
            _context = context;
        }

        public async Task<Proveedor> CreateProveedor(Proveedor proveedor)
        {
            _context.Proveedors.Add(proveedor);
            await _context.SaveChangesAsync();
            return proveedor;
        }

        public async Task DeleteProveedor(int id)
        {
            var proveedor = await _context.Proveedors.FirstOrDefaultAsync(u => u.Id == id);
            if (proveedor == null) throw new KeyNotFoundException("proveedor no encontrado");
            {
                _context.Proveedors.Remove(proveedor);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Proveedor> GetProveedorById(int id)
        {
            return await _context.Proveedors.FindAsync(id);
        }

        public async Task<IEnumerable<Proveedor>> GetProveedors()
        {
            return await _context.Proveedors.ToListAsync();
        }

        public async Task UpdateProveedor(Proveedor proveedor, int id)
        {

            var proveedorExistente = await _context.Proveedors.FirstOrDefaultAsync(u => u.Id == id);
            if (proveedorExistente == null) throw new KeyNotFoundException("proveedor no encontrado");
            proveedorExistente.Nombre = proveedor.Nombre;
            proveedorExistente.Apellido = proveedor.Apellido;
            proveedorExistente.TipoDePersona = proveedor.TipoDePersona;
            proveedorExistente.Dui = proveedor.Dui;
            proveedorExistente.NombreEmpresa = proveedor.NombreEmpresa;
            proveedorExistente.Nrc = proveedor.Nrc;
            proveedorExistente.Contacto = proveedor.Contacto;
            proveedorExistente.Telefono = proveedor.Telefono;
            proveedorExistente.Direccion = proveedor.Direccion;
            await _context.SaveChangesAsync();
        }
    }
}
