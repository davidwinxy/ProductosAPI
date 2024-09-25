using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductosAPI.Models;
using ProductosAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Protege todos los métodos de este controlador
    public class CompraController : ControllerBase
    {
        private readonly ICompraService _compraService;

        public CompraController(ICompraService compraService)
        {
            _compraService = compraService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Compra>>> GetCompras()
        {
            var compras = await _compraService.GetCompras();
            return Ok(compras);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Compra>> GetCompra(int id)
        {
            var compra = await _compraService.GetCompraById(id);
            if (compra == null)
            {
                return NotFound();
            }
            return Ok(compra);
        }

        [HttpPost]
        public async Task<ActionResult<Compra>> CreateCompra(Compra compra)
        {
            // Validar que el proveedor existe
            if (!await _compraService.ExisteProveedor(compra.IdProveedor))
            {
                return BadRequest("Proveedor no encontrado.");
            }

            // Validación de cantidad
            if (compra.Cantidad <= 0)
            {
                return BadRequest("La cantidad debe ser mayor que cero.");
            }

            // Cálculo de totales
            compra.CalcularTotales();

            // Crear la compra
            var compraCreada = await _compraService.CreateCompra(compra);
            return CreatedAtAction(nameof(GetCompra), new { id = compraCreada.Id }, compraCreada);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompra(int id, Compra compra)
        {
            // Validación de cantidad
            if (compra.Cantidad <= 0)
            {
                return BadRequest("La cantidad debe ser mayor que cero.");
            }

            // Cálculo de totales
            compra.CalcularTotales();

            await _compraService.UpdateCompra(compra, id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompra(int id)
        {
            await _compraService.DeleteCompra(id);
            return NoContent();
        }
    }
}
