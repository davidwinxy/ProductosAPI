using System;
using Microsoft.EntityFrameworkCore;
using ProductosAPI.Context;
using ProductosAPI.Models;
using ProductosAPI.Services.Interfaces;

namespace ProductosAPI.Services.Implementaciones;

public class LoginService : ILoginService
{
        private readonly ProductosContext _context;

        public LoginService(ProductosContext context)
        {
            _context = context;
        }

        public async Task<Login> GetByEmailAsync(string email)
        {
            // Cambiado a 'Login' para coincidir con el nombre del DbSet
            return await _context.Login.FirstOrDefaultAsync(l => l.username == email);
        }

        public async Task CreateAsync(Login login) // Método para crear un nuevo login
        {
            await _context.Login.AddAsync(login); // Usar 'Login' en lugar de 'Logins'
            await _context.SaveChangesAsync();
        }
}
