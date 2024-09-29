using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductosAPI.Models;
using ProductosAPI.Services.Interfaces;

[Route("api/[controller]")]
[ApiController]
public class ImagenArticuloController : ControllerBase
{
    private readonly IImagenArticuloInterface _imagenArticuloService;

    public ImagenArticuloController(IImagenArticuloInterface imagenArticuloService)
    {
        _imagenArticuloService = imagenArticuloService;
    }

    [HttpPost]
    public async Task<ActionResult> UploadImage(int articuloId, IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No se ha proporcionado ningún archivo.");
        }

        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        var imagenArticulo = new ImagenArticulo
        {
            ArticuloId = articuloId,
            FileName = file.FileName,
            ImageData = memoryStream.ToArray(),
            ContentType = file.ContentType
        };

        await _imagenArticuloService.AddAsync(imagenArticulo);
        return Ok("Imagen subida correctamente.");
    }
}

