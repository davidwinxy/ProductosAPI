using Microsoft.AspNetCore.Mvc;
using ProductosAPI.Models;
using ProductosAPI.Services.Implementaciones;
using ProductosAPI.Services.Interfaces;


namespace ProductosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioRolController : ControllerBase
    {
        private readonly IUsuarioRolService _usuariorol;

        public UsuarioRolController(IUsuarioRolService usuariorol)
        {
            _usuariorol = usuariorol;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetUsuarioRol(int id)
        {
            var usuariorol = await _usuariorol.GetUsuarioRolById(id);
            if (usuariorol == null)
            {
                return NotFound();
            }
            return Ok(usuariorol);
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioRol>> CreateUsuarioRol(UsuarioRol usuariorol)
        {
            var usuariorolcreado = await _usuariorol.CreateUsuarioRol(usuariorol);
            return CreatedAtAction(nameof(GetUsuarioRol), new { id = usuariorolcreado.Id }, usuariorolcreado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuarioRol(int id, UsuarioRol usuariorol)
        {
            await _usuariorol.UpdateUsuarioRol(usuariorol, id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task <IActionResult> DeleteUsuarioRol(int id)
        {
            await _usuariorol.DeleteUsuarioRol(id);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioRol>>> GetUsuarioRol()
        {

            var usuariorol = await _usuariorol.GetUsuarioRol();
            return Ok(usuariorol);
        }
    }
}
