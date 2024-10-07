using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MYABackend.Repositories;
using MYABackend.Responses;
using MYABackend.Models;
using System.Net;
using System.Text.Json;

namespace MYABackend.Controllers;

[ApiController]
public class ProductoController : ControllerBase
{
    private Repository repository = new Repository();

    [HttpGet]
    [Route("ProductoController/Get")]
    [AllowAnonymous]
    public async Task<BaseResponse> Get()
    {
        try
        {
            var rta = await repository.GetListFromProcedure<dynamic>("obtenerProducto");
            return new DataResponse<dynamic>(true, (int)HttpStatusCode.OK, "Lista entidad", data: rta);
        }
        catch (Exception ex)
        {
            return new BaseResponse(false, (int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    [HttpGet]
    [Route("ProductoController/GetTalle")]
    [Authorize(Policy ="Admin")]
    public async Task<BaseResponse> GetTalle()
    {
        try
        {
            var rta = await repository.GetListFromProcedure<dynamic>("obtenerTalle");
            return new DataResponse<dynamic>(true, (int)HttpStatusCode.OK, "Lista entidad", data: rta);
        }
        catch (Exception ex)
        {
            return new BaseResponse(false, (int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    
    [HttpGet]
    [Route("ProductoController/GetMarca")]
    [Authorize(Policy ="Admin")]
    public async Task<BaseResponse> GetMarca()
    {
        try
        {
            var rta = await repository.GetListFromProcedure<dynamic>("obtenerMarca");
            return new DataResponse<dynamic>(true, (int)HttpStatusCode.OK, "Lista entidad", data: rta);
        }
        catch (Exception ex)
        {
            return new BaseResponse(false, (int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    
    [HttpGet]
    [Route("ProductoController/GetCategoria")]
    [Authorize(Policy ="Admin")]
    public async Task<BaseResponse> GetCategoria()
    {
        try
        {
            var rta = await repository.GetListFromProcedure<dynamic>("obtenerCategoria");
            return new DataResponse<dynamic>(true, (int)HttpStatusCode.OK, "Lista entidad", data: rta);
        }
        catch (Exception ex)
        {
            return new BaseResponse(false, (int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    [HttpPost]
    [Route("ProductoController/PostU")]
    [Authorize(Policy ="Admin")]
    public async Task<IActionResult> PostU([FromForm] Producto upload)
    {
        if (upload == null || upload.File.Length == 0) return BadRequest("No se proporcionó ningún archivo.");

        var files = new List<string>();
        foreach (var file in upload.File)
        {
            if (file.Length > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine("MYA-frontend", "assets", "img", fileName);
                try
                {
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    files.Add(Path.Combine("assets", "img", fileName)); // Guardar la ruta relativa
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Error al subir el archivo: {ex.Message}");
                }
            }
        }

        upload.RutaImagen = JsonSerializer.Serialize(files);

        if (string.IsNullOrEmpty(upload.Nombre) || string.IsNullOrEmpty(upload.Descripcion) ||
           upload.IdCategoria == 0 || upload.IdMarca == 0 || upload.Precio <= 0 ||
           upload.Stock <= 0 || string.IsNullOrEmpty(upload.RutaImagen))
        {
            return BadRequest("No se proporcionó todos los datos necesarios.");
        }

        await repository.ExecuteProcedure("crearProducto",upload.crearProducto());

        return Ok(new { files });
    }
}