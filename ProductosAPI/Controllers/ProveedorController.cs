using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductosAPI.Services.Interfaces;
using ProductosAPI.Models;

namespace ProductosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        private readonly IProveedorService _proveedorServices;

        public ProveedorController(IProveedorService proveedorService)
        {
            _proveedorServices = proveedorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proveedor>>> GetProveedors()
        {

            var proveedors = await _proveedorServices.GetProveedors();
            return Ok(proveedors);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> GetProveedors(int id)
        {
            var proveedor = await _proveedorServices.GetProveedorById(id);
            if (proveedor == null)
            {
                return NotFound();
            }
            return Ok(proveedor);
        }


        [HttpPost]
        public async Task<ActionResult<Proveedor>> CreateProveedor(Proveedor proveedor)
        {
            var proveedorCreado = await _proveedorServices.CreateProveedor(proveedor);
            return CreatedAtAction(nameof(GetProveedors), new { id = proveedorCreado.Id }, proveedorCreado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProveedor(int id, Proveedor proveedor)
        {
            await _proveedorServices.UpdateProveedor(proveedor, id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProveedor(int id)
        {
            await _proveedorServices.DeleteProveedor(id);
            return NoContent();
        }

    }
}
