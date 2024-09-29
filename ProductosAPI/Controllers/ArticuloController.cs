using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductosAPI.DTO;
using ProductosAPI.Models;
using ProductosAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticuloController : ControllerBase
    {
        private readonly IArticuloInterface _articuloService;
        private readonly IImagenArticuloInterface _imagenArticuloService;

        public ArticuloController(IArticuloInterface articuloService, IImagenArticuloInterface imagenArticuloService)
        {
            _articuloService = articuloService;
            _imagenArticuloService = imagenArticuloService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Articulo>>> GetArticulos()
        {
            var articulos = await _articuloService.GetAllAsync();

            foreach (var articulo in articulos)
            {
                // Para cada artículo, obtener sus imágenes
                articulo.Imagenes = (ICollection<ImagenArticulo>)await _imagenArticuloService.GetByArticuloIdAsync(articulo.Id);
            }

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

            // Obtener las imágenes relacionadas con el artículo
            articulo.Imagenes = (ICollection<ImagenArticulo>)await _imagenArticuloService.GetByArticuloIdAsync(id);

            return Ok(articulo);
        }


        [HttpPost]
        public async Task<ActionResult<Articulo>> CreateArticulo([FromBody] ArticuloCreateUpdateDTO articuloDTO)
        {
            if (string.IsNullOrWhiteSpace(articuloDTO.Nombre) || string.IsNullOrWhiteSpace(articuloDTO.Descripcion))
            {
                return BadRequest("Nombre y Descripción son obligatorios.");
            }

            var articulo = new Articulo
            {
                Nombre = articuloDTO.Nombre,
                Descripcion = articuloDTO.Descripcion,
                Categoria = articuloDTO.Categoria,
                Disponibilidad = articuloDTO.Disponibilidad,
            };

            Articulo articuloCreado = await _articuloService.CreateArticulo(articulo);
            return CreatedAtAction(nameof(GetArticulo), new { id = articuloCreado.Id }, articuloCreado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArticulo(int id, [FromBody] ArticuloCreateUpdateDTO articuloDTO)
        {
            // Obtener el artículo existente por su ID
            var articuloExistente = await _articuloService.GetByIdAsync(id);

            // Verificar si el artículo existe
            if (articuloExistente == null)
            {
                return NotFound($"El artículo con ID {id} no existe.");
            }

            // Actualizar las propiedades del artículo existente
            articuloExistente.Nombre = articuloDTO.Nombre;
            articuloExistente.Descripcion = articuloDTO.Descripcion;
            articuloExistente.Categoria = articuloDTO.Categoria;
            articuloExistente.Disponibilidad = articuloDTO.Disponibilidad;

            // Guardar los cambios en la base de datos
            await _articuloService.UpdateAsync(articuloExistente, id);

            return NoContent(); // Retornar un código 204 NoContent
        }



        // Eliminar un artículo
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticulo(int id)
        {
            await _articuloService.DeleteAsync(id);
            return NoContent();
        }
    }
}
