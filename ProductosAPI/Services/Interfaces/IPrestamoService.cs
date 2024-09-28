using System;
using ProductosAPI.Models;

namespace ProductosAPI.Services.Interfaces;

public interface IPrestamoService
{
        Task<Prestamo> CreatePrestamo(Prestamo prestamo);
        Task<bool> DeletePrestamo(int id);
        Task<IEnumerable<Prestamo>> GetAllPrestamos();
        Task<Prestamo> GetPrestamoById(int id);
        Task<bool> UpdatePrestamo(Prestamo prestamo);
        Task<bool> MarkPrestamoAsReturned(int id);
}
