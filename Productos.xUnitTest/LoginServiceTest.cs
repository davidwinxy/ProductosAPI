using System;
using Microsoft.EntityFrameworkCore;
using ProductosAPI.Context;
using ProductosAPI.Models;
using ProductosAPI.Services.Implementaciones;

namespace Productos.xUnitTest;

public class LoginServiceTest
{
    private readonly LoginService _loginService;
        private readonly ProductosContext _productosContext;

        public LoginServiceTest()
        {
            _productosContext = ProductoAPIContextMemory<ProductosContext>.CreateDbContext(Guid.NewGuid().ToString());
            _loginService = new LoginService(_productosContext);
        }

        [Fact]
        public async Task CreateLogin()
        {
            var login = new Login
            {
                username = "testUser",
                password = "TestPassword123"
            };

            await _loginService.CreateAsync(login);
            var loginFromDb = await _productosContext.Login.FirstOrDefaultAsync(l => l.username == "testUser");

            Assert.NotNull(loginFromDb);
            Assert.Equal("testUser", loginFromDb.username);
            Assert.Equal("TestPassword123", loginFromDb.password);
        }

        [Fact]
        public async Task GetLoginByEmail()
        {
            var login = new Login
            {
                username = "testUser",
                password = "TestPassword123"
            };

            await _productosContext.Login.AddAsync(login);
            await _productosContext.SaveChangesAsync();

            var result = await _loginService.GetByEmailAsync("testUser");

            Assert.NotNull(result);
            Assert.Equal("testUser", result.username);
            Assert.Equal("TestPassword123", result.password);
        }
}
