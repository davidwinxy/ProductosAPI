using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductosAPI.Models;
using ProductosAPI.Services.Implementaciones;
using ProductosAPI.Services.Interfaces;

namespace ProductosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<ActionResult> GetCompras(int id)
        {
            var proveedor = await _compraService.GetCompraById(id);
            if (proveedor == null)
            {
                return NotFound();
            }
            return Ok(proveedor);
        }

        //corregir lo de proveedores para que el input guarde el id

        [HttpPost]
        public async Task<ActionResult<Compra>> CreateCompra(Compra compra, int idProveedor)
        {
            var compraCreada = await _compraService.CreateCompra(compra, idProveedor);
            return CreatedAtAction(nameof(GetCompras), new { id = compraCreada.Id }, compraCreada);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompra(int id, Compra compra)
        {
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
