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

        public async Task<Compra> CreateCompra(Compra compra, int idProveedor)
        {
            

            await _context.compras.AddAsync(compra);

            await _context.SaveChangesAsync();

            return compra;
        }


        public async Task DeleteCompra(int id)
        {
            var compra = await _context.compras.FirstOrDefaultAsync(u => u.Id == id);
            if (compra == null) throw new KeyNotFoundException("compra no encontrada");
            {
                _context.compras.Remove(compra);
                await _context.SaveChangesAsync();
            }
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
            if (compraExistente == null) throw new KeyNotFoundException("compra no encontrada");
            compraExistente.NumeroDeFactura = compra.NumeroDeFactura;
            compraExistente.Total=compra.Total;
            compraExistente.SubTotal=compra.SubTotal;
            compraExistente.Fecha=compra.Fecha;
            compraExistente.Iva = compra.Iva;
            compraExistente.IdProveedor = compra.IdProveedor;
            await _context.SaveChangesAsync();

        }
    }
}
