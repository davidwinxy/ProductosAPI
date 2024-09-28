using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductosAPI.Models;
using ProductosAPI.Services.Implementaciones;
using ProductosAPI.Services.Interfaces;

namespace ProductosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticuloController : ControllerBase
    {

        private readonly IArticuloInterface _articuloService;
        private readonly IImagenArticuloInterface _imagenArticuloInterface;

        public ArticuloController(IArticuloInterface articuloInterface, IImagenArticuloInterface imagenArticuloInterface)
        {
            _articuloService = articuloInterface;
            _imagenArticuloInterface = imagenArticuloInterface;
        }

        // Obtener todos los artículos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Articulo>>> GetArticulos()
        {
            var articulos = await _articuloService.GetAllAsync();
            return Ok(articulos);
        }

        // Obtener un artículo por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Articulo>> GetArticulo(int id)
        {
            var articulo = await _articuloService.GetByIdAsync(id);
            if (articulo == null)
            {
                return NotFound();
            }
            return Ok(articulo);
        }

        [HttpPost]
        public async Task<ActionResult<Articulo>> CreateArticulo(Articulo articulo)
        {
            if (string.IsNullOrWhiteSpace(articulo.Nombre) || string.IsNullOrWhiteSpace(articulo.Descripcion))
            {
                return BadRequest("Nombre y Descripción son obligatorios.");
            }

            // Crear el artículo
            Articulo articuloCreado = await _articuloService.CreateArticulo(articulo);
            return CreatedAtAction(nameof(GetArticulo), new { id = articuloCreado.Id }, articuloCreado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArticulo(int id, Articulo articulo)
        {
            
            await _articuloService.UpdateAsync(articulo, id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticulo(int id)
        {
            await _articuloService.DeleteAsync(id);
            return NoContent();
        }
    }
}
