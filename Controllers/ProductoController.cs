using Microsoft.AspNetCore.Authorization;
using MYABackend.Models.Productos;
using Microsoft.AspNetCore.Mvc;
using MYABackend.Repositories;
using MYABackend.Responses;
using MYABackend.Models;
using System.Text.Json;
using System.Net;
using Dapper;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;

namespace MYABackend.Controllers;

[ApiController]
public class ProductoController : ControllerBase
{
    private static Dictionary<int, List<ProductoCarrito>> paginacion = new Dictionary<int, List<ProductoCarrito>>();
    private static Dictionary<int, List<ProductosById>> productos = new Dictionary<int, List<ProductosById>>();
    private Repository repository = new Repository();

    [HttpGet]
    [Route("ProductoController/Get")]
    [AllowAnonymous]
    public async Task<BaseResponse> Get(int? page)
    {
        int pageSize = 10;
        int pageNumber = page ?? 1;

        if (paginacion.ContainsKey(pageNumber - 1))
        {
            paginacion.TryGetValue(pageNumber - 1, out List<ProductoCarrito> producto);
            return new DataResponse<List<ProductoCarrito>>(true, (int)HttpStatusCode.OK, "Lista entidad", data: producto);
        }

        var dp = new DynamicParameters();
        dp.Add("@Offset", (pageNumber - 1) * pageSize);
        dp.Add("@PageSize", pageSize);

        try
        {
            var rta = await repository.GetListFromProcedure<ProductoCarrito>("obtenerProductos", dp);

            paginacion.Add(pageNumber - 1, rta);

            return new DataResponse<List<ProductoCarrito>>(true, (int)HttpStatusCode.OK, "Lista entidad", data: rta);
        }
        catch (Exception ex)
        {
            return new BaseResponse(false, (int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    [HttpGet]
    [Route("ProductoController/GetTotalDeProductos")]
    [AllowAnonymous]
    public async Task<BaseResponse> GetTotalDeProductos()
    {
        try
        {
            var rta = await repository.GetListFromProcedure<dynamic>("obtenerTotalDeProductos");
            return new DataResponse<dynamic>(true, (int)HttpStatusCode.OK, "Lista entidad", data: rta);
        }
        catch (Exception ex)
        {
            return new BaseResponse(false, (int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    [HttpGet]
    [Route("ProductoController/GetById")]
    [AllowAnonymous]
    public async Task<BaseResponse> GetById(int id)
    {

        if (productos.ContainsKey(id))
        {
            productos.TryGetValue(id, out List<ProductosById> producto);
            return new DataResponse<List<ProductosById>>(true, (int)HttpStatusCode.OK, "Lista entidad", data: producto);
        }

        try
        {
            var dp = new DynamicParameters();
            dp.Add("@Id", id);
            var rta = await repository.GetListFromProcedure<ProductosById>("obtenerProductoById", dp);

            productos.Add(id, rta);

            return new DataResponse<dynamic>(true, (int)HttpStatusCode.OK, "Lista entidad", data: rta);
        }
        catch (Exception ex)
        {
            return new BaseResponse(false, (int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    [HttpGet]
    [Route("ProductoController/GetTalle")]
    [Authorize(Policy = "Admin")]
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
    [Authorize(Policy = "Admin")]
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
    [Authorize(Policy = "Admin")]
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

    [HttpPatch]
    [Route("ProductoController/Modificar")]
    [Authorize(Policy = "Admin")]
    public async Task<BaseResponse> Patch([FromBody] Producto upload)
    {
        if (upload == null || upload.IdProducto <= 0) return new BaseResponse(false, (int)HttpStatusCode.InternalServerError, "No hay ID");
        Producto p;
        try
        {
            var dp = new DynamicParameters();
            dp.Add("@Id", upload.IdProducto);
            List<Producto> rsp = await repository.GetListFromProcedure<Producto>("obtenerProductoParaModificar", dp);
            p = rsp.FirstOrDefault();
        }
        catch (Exception ex)
        {
            return new BaseResponse(false, (int)HttpStatusCode.InternalServerError, ex.Message);
        }

        try
        {
            var idProducto = string.IsNullOrWhiteSpace(Convert.ToString(upload.IdProducto)) ? p.IdProducto : upload.IdProducto;
            var idCategoria = string.IsNullOrWhiteSpace(Convert.ToString(upload.IdCategoria)) ? p.IdCategoria : upload.IdCategoria;
            var idMarca = string.IsNullOrWhiteSpace(Convert.ToString(upload.IdMarca)) ? p.IdMarca : upload.IdMarca;
            var idTalle = string.IsNullOrWhiteSpace(Convert.ToString(upload.IdTalle)) ? p.IdMarca : upload.IdTalle;
            var nombre = string.IsNullOrWhiteSpace(Convert.ToString(upload.Nombre)) ? p.Nombre : upload.Nombre;
            var descripcion = string.IsNullOrWhiteSpace(upload.Descripcion) ? p.Descripcion : upload.Descripcion;
            var precio= string.IsNullOrWhiteSpace(Convert.ToString(upload.IdTalle)) ? p.Precio : upload.Precio;
            var stock = string.IsNullOrWhiteSpace(Convert.ToString(upload.Stock)) ? p.Stock : upload.Stock;
            var rutaImagen = string.IsNullOrWhiteSpace(upload.RutaImagen) ? p.RutaImagen : upload.RutaImagen;

            var dp = new DynamicParameters();
            dp.Add("@IdProducto", idProducto);
            dp.Add("@IdCategoria", idCategoria);
            dp.Add("@IdMarca", idMarca);
            dp.Add("@IdTalle", idTalle);
            dp.Add("@Nombre", nombre);
            dp.Add("@Descripcion", descripcion);
            dp.Add("@Precio", precio);
            dp.Add("@Stock", stock);
            dp.Add("@RutaImagen", rutaImagen);

            var rsp = await repository.ExecuteProcedure("actualizarProducto", dp);

            return new DataResponse<dynamic>(true, (int)HttpStatusCode.OK, "Lista entidad", data: rsp);
        } catch (Exception ex){
            return new BaseResponse(false, (int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    [HttpPost]
    [Route("ProductoController/PostU")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> PostU([FromForm] Producto upload)
    {
        if (upload == null || upload.File.Length == 0) return BadRequest("No se proporcionó ningún archivo.");

        var files = new List<string>();

        string currentDirectory = Directory.GetCurrentDirectory();
        string cleanedDirectory = currentDirectory.Replace("MYA-backend", "MYA-frontend");
        var basePath = Path.Combine(cleanedDirectory, "src", "Assets", "Images", "Products");

        if (!Directory.Exists(basePath)) Directory.CreateDirectory(basePath);

        try
        {
            int i = 1;
            foreach (var file in upload.File)
            {
                if (file.Length > 0)
                {
                    string newFileName = $"Campus-{i:D2}{Path.GetExtension(file.FileName)}";
                    var path = Path.Combine(basePath, newFileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    string relativePath = Path.Combine("src", "Assets", "Images", "Products", newFileName).Replace("\\", "/");
                    files.Add(relativePath);
                    i++;
                }
            }

            upload.RutaImagen = JsonSerializer.Serialize(files);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error al subir el archivo: {ex.Message}");
        }

        if (string.IsNullOrEmpty(upload.Nombre) || string.IsNullOrEmpty(upload.Descripcion) ||
            upload.IdCategoria == 0 || upload.IdMarca == 0 || upload.Precio <= 0 ||
            upload.Stock <= 0 || string.IsNullOrEmpty(upload.RutaImagen))
        {
            return BadRequest("No se proporcionó todos los datos necesarios.");
        }

        try
        {
            await repository.ExecuteProcedure("crearProducto", upload.crearProducto());
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error en la base de Datos: {ex.Message}");
        }

        return Ok(new { files });
    }
}