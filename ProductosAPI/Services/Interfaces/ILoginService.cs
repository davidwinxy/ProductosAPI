using System;
using ProductosAPI.Models;

namespace ProductosAPI.Services.Interfaces;

public interface ILoginService
{
    
        Task<Login> GetByEmailAsync(string username); // Obtener Login por correo
        Task CreateAsync(Login login); // Crear un nuevo Login
}
