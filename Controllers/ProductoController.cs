using Microsoft.AspNetCore.Authorization;
using MYABackend.Models.Productos;
using Microsoft.AspNetCore.Mvc;
using MYABackend.Repositories;
using MYABackend.Responses;
using MYABackend.Models;
using System.Text.Json;
using System.Net;
using Dapper;
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

        if (paginacion.ContainsKey(pageNumber-1))
        {
            paginacion.TryGetValue(pageNumber-1, out List<ProductoCarrito> producto);
            return new DataResponse<List<ProductoCarrito>>(true, (int)HttpStatusCode.OK, "Lista entidad", data: producto);
        }

        var dp = new DynamicParameters();
        dp.Add("@Offset",(pageNumber-1) * pageSize);
        dp.Add("@PageSize",pageSize);

        try
        {
            var rta = await repository.GetListFromProcedure<ProductoCarrito>("obtenerProductos",dp);

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
            dp.Add("@Id",id);
            var rta = await repository.GetListFromProcedure<ProductosById>("obtenerProductoById",dp);

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
    public async Task<BaseResponse> PostU([FromForm] Producto upload)
    {
        if (upload == null || upload.File.Length == 0) return new BaseResponse(false, (int) HttpStatusCode.InternalServerError, "No se proporcionó ningún archivo.");

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
                    string newFileName = $"{upload.Nombre}-{i:D2}{Path.GetExtension(file.FileName)}";
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
            return new BaseResponse(false, (int)HttpStatusCode.InternalServerError, ex.Message);
        }

        if (string.IsNullOrEmpty(upload.Nombre) || string.IsNullOrEmpty(upload.Descripcion) ||
            upload.IdCategoria == 0 || upload.IdMarca == 0 || upload.Precio <= 0 ||
            upload.Stock <= 0 || string.IsNullOrEmpty(upload.RutaImagen))
        {
            return new BaseResponse(false, (int)HttpStatusCode.InternalServerError, "No se proporcionó todos los datos necesarios.");
        }

        try
        {
            await repository.ExecuteProcedure("crearProducto", upload.crearProducto());
        }
        catch (Exception ex)
        {
            return new BaseResponse(false, (int)HttpStatusCode.InternalServerError, $"Error en la base de Datos: {ex.Message}");
        }
        return new DataResponse<dynamic>(true, (int)HttpStatusCode.OK, "Lista entidad", data: files);
    }
}