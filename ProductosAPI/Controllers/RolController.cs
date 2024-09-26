using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductosAPI.Models;
using ProductosAPI.Services.Implementaciones;
using ProductosAPI.Services.Interfaces;


namespace ProductosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] 
    public class RolController : ControllerBase
    {
        private readonly IRolService _rolServices;

        public RolController(IRolService rolService)
        {
            _rolServices = rolService;
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult> GetRol(int id)
        {
            var rol = await _rolServices.GetRolById(id);
            if (rol == null)
            {
                return NotFound();
            }
            return Ok(rol);
        }


        [HttpPost]
        public async Task<ActionResult<Rol>> CreateRol(Rol rol)
        {
            var rolCreado = await _rolServices.CreateRol(rol);
            return CreatedAtAction(nameof(GetRol), new { id = rolCreado.Id }, rolCreado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRol(int id, Rol rol)
        {
            await _rolServices.UpdateRol(rol, id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRol(int id)
        {
            await _rolServices.DeleteRol(id);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proveedor>>> GetRol()
        {

            var rol = await _rolServices.GetRol();
            return Ok(rol);
        }
    }

}
