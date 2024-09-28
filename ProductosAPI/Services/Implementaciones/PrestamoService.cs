using System;
using ProductosAPI.Models;
using ProductosAPI.Context;
using ProductosAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ProductosAPI.Services.Implementaciones
{
    public class PrestamoService : IPrestamoService
    {
        private readonly ProductosContext _context;

        public PrestamoService(ProductosContext context)
        {
            _context = context;
        }

        public async Task<Prestamo> CreatePrestamo(Prestamo prestamo)
        {
            var articulo = await _context.articulo.FindAsync(prestamo.Articulo_Id);
            if (articulo == null || !articulo.Disponibilidad)
                throw new InvalidOperationException("El artículo no está disponible.");

            articulo.Disponibilidad = false;
            await _context.SaveChangesAsync();

            await _context.Prestamos.AddAsync(prestamo);
            await _context.SaveChangesAsync();

            return prestamo;
        }

        public async Task<bool> DeletePrestamo(int id)
        {
            var prestamo = await _context.Prestamos.FindAsync(id);
            if (prestamo == null)
                return false;

            _context.Prestamos.Remove(prestamo);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Prestamo>> GetAllPrestamos()
        {
            return await _context.Prestamos.ToListAsync();
        }

        public async Task<Prestamo> GetPrestamoById(int id)
        {
            return await _context.Prestamos.FindAsync(id);
        }

        public async Task<bool> UpdatePrestamo(Prestamo prestamo)
        {
            _context.Prestamos.Update(prestamo);
            return await _context.SaveChangesAsync() > 0;        
        }

    public async Task<bool> MarkPrestamoAsReturned(int id)
    {
    var prestamo = await _context.Prestamos.FindAsync(id);
    if (prestamo == null || prestamo.Estado == "Devuelto")
    {
        return false; 
    }

    prestamo.Estado = "Devuelto"; 
    prestamo.Fecha_devolucion = DateTime.UtcNow; 

    _context.Prestamos.Update(prestamo); 
    await _context.SaveChangesAsync(); 
    return true;
    }


    }
}