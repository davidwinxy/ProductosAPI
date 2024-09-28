using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductosAPI.Models;
using ProductosAPI.Services.Interfaces;

namespace ProductosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamoController : ControllerBase
    {
        private readonly IPrestamoService _prestamoService;

        public PrestamoController(IPrestamoService prestamoService)
        {
            _prestamoService = prestamoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prestamo>>> GetPrestamos()
        {
            var prestamos = await _prestamoService.GetAllPrestamos();
            return Ok(prestamos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Prestamo>> GetPrestamo(int id)
        {
            var prestamo = await _prestamoService.GetPrestamoById(id);
            if (prestamo == null)
            {
                return NotFound();
            }
            return Ok(prestamo);
        }

        [HttpPost]
        public async Task<ActionResult<Prestamo>> CreatePrestamo([FromBody] Prestamo prestamo)
        {
            if (prestamo == null)
            {
                return BadRequest("El préstamo no puede ser nulo.");
            }

            try
            {
                prestamo.Estado = "En Prestamo";

                var createdPrestamo = await _prestamoService.CreatePrestamo(prestamo);
                return CreatedAtAction(nameof(GetPrestamo), new { id = createdPrestamo.Id }, createdPrestamo);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al crear el préstamo.", error = ex.Message });
            }
        }

        [HttpPatch("Devolver/{id}")]
        public async Task<IActionResult> DevolverPrestamo(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID inválido.");
            }

            try
            {
                var result = await _prestamoService.MarkPrestamoAsReturned(id);
                if (!result)
                {
                    return BadRequest("Error al devolver el artículo o ya ha sido devuelto.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al procesar la solicitud." });
            }
        }
    }
}