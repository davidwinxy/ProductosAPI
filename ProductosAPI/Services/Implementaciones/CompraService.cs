using Microsoft.EntityFrameworkCore;
using ProductosAPI.Context;
using ProductosAPI.Models;
using ProductosAPI.Services.Interfaces;

namespace ProductosAPI.Services.Implementaciones
{
    public class CompraService : ICompraService
    {
        private readonly ProductosContext _context;

        public CompraService(ProductosContext context)
        {
            _context = context;
        }

        public async Task<Compra> CreateCompra(Compra compra)
        {
            // Validar que el proveedor existe
            var proveedorExistente = await _context.Proveedors.AnyAsync(p => p.Id == compra.IdProveedor);
            if (!proveedorExistente)
            {
                throw new KeyNotFoundException("Proveedor no encontrado");
            }

            // Calcular totales antes de guardar
            compra.CalcularTotales();

            // Agregar la compra al contexto y guardar
            await _context.compras.AddAsync(compra);
            await _context.SaveChangesAsync();

            return compra;
        }

        public async Task DeleteCompra(int id)
        {
            var compra = await _context.compras.FirstOrDefaultAsync(u => u.Id == id);
            if (compra == null)
                throw new KeyNotFoundException("Compra no encontrada");

            _context.compras.Remove(compra);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExisteProveedor(int idProveedor)
        {
            return await _context.Proveedors.AnyAsync(p => p.Id == idProveedor);
        }

        public async Task<Compra> GetCompraById(int id)
        {
            return await _context.compras.FindAsync(id);
        }

        public async Task<IEnumerable<Compra>> GetCompras()
        {
            return await _context.compras.ToListAsync();
        }

        public async Task UpdateCompra(Compra compra, int id)
        {
            var compraExistente = await _context.compras.FirstOrDefaultAsync(u => u.Id == id);
            if (compraExistente == null)
                throw new KeyNotFoundException("Compra no encontrada");

            // Actualizar solo los campos que se pueden modificar
            compraExistente.NumerodeFactura = compra.NumerodeFactura; // Asegúrate de que esta propiedad exista
            compraExistente.Cantidad = compra.Cantidad; // Esto afecta los cálculos, así que lo recalculamos
            compraExistente.PrecioUnitario = compra.PrecioUnitario; // Asegúrate de que esta propiedad exista

            // Calcular los totales después de modificar los campos
            compraExistente.CalcularTotales();

            // Guardar cambios
            await _context.SaveChangesAsync();
        }
    }
}
