using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductosAPI.Models;
using ProductosAPI.Services.Implementaciones;
using ProductosAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IusuarioService _usuarioService;

        public UsuarioController(IusuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<usuario>>> GetUsuarios()
        {
            var usuarios = await _usuarioService.GetUsuarios();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<usuario>> GetUsuarioById(int id)
        {
            var usuario = await _usuarioService.GetUsuariosById(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }




        [HttpPost]
        public async Task<ActionResult<usuario>> CreateUsuario(usuario usuario)
        {
            var usuarioCreado = await _usuarioService.CreateUsuario(usuario);
            return CreatedAtAction(nameof(GetUsuarioById), new { id = usuarioCreado.Id }, usuarioCreado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, usuario usuario)
        {
            
            await _usuarioService.UpdateUsuario(usuario, id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            await _usuarioService.DeleteUsuario(id);
            return NoContent();
        }


    }
}
