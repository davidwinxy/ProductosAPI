﻿using Microsoft.EntityFrameworkCore;
using ProductosAPI.Context;
using ProductosAPI.Models;
using ProductosAPI.Services.Interfaces;

namespace ProductosAPI.Services.Implementaciones
{
    public class ArticuloService : IArticuloInterface
    {
        private readonly ProductosContext _context;

        public ArticuloService(ProductosContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Articulo>> GetAllAsync()
        {
            return await _context.articulo.ToListAsync();
        }

        public async Task<Articulo> GetByIdAsync(int id)
        {
            return await _context.articulo
                                 .Include(a => a.Imagenes) // Incluir las imágenes asociadas
                                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task UpdateAsync(Articulo articulo, int id)
        {
            var existingArticulo = await _context.articulo.FindAsync(id);
            if (existingArticulo != null)
            {
                // Actualiza las propiedades del artículo existente
                existingArticulo.Nombre = articulo.Nombre;
                existingArticulo.Descripcion = articulo.Descripcion;
                existingArticulo.Categoria = articulo.Categoria;
                existingArticulo.Disponibilidad = articulo.Disponibilidad;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var articulo = await _context.articulo.FindAsync(id);
            if (articulo != null)
            {
                _context.articulo.Remove(articulo);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Articulo> CreateArticulo(Articulo articulo)
        {
            await _context.articulo.AddAsync(articulo);
            await _context.SaveChangesAsync();
            return articulo;
        }

        public async Task<IEnumerable<ImagenArticulo>> GetByArticuloIdAsync(int articuloId)
        {
            return await _context.imagenArticulo
                                .Where(img => img.ArticuloId == articuloId)
                                .ToListAsync();
        }
    }
}