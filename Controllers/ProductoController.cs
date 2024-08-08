
using Dapper;
using Microsoft.AspNetCore.Mvc;
using MYABackend.Models;
using MYABackend.Responses;
using SistemasCafeBackEnd.Repositories;
using System.Net;
using System.Data;

namespace MYABackend.Controllers;

public class ProductoController : ControllerBase
{
    private Repository repository = new Repository();
    [HttpGet]
    [Route("ProductoController/Get")]
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

    [HttpPost]
    [Route("ProductoController/Post")]
    public async Task<IActionResult> Post([FromForm] Producto upload)
    {
        if (upload == null || upload.File.Length == 0) return BadRequest("No se proporcionó ningún archivo.");

        //var path = Path.Combine("C:\\Users\\PC\\MYA-frontend\\assets\\img", upload.File.FileName);
        var path = Path.Combine("C:\\Users\\Usuario\\MYA-frontend\\assets\\img", upload.File.FileName);

        using (var stream = new FileStream(path, FileMode.Create))
        {
            await upload.File.CopyToAsync(stream);
        }

        var file = "MYA-frontend\\assets\\img" + upload.File.FileName;

        upload.RutaImagen = file;

        if (upload.Nombre == null || upload.Descripcion == null || upload.IdCategoria == 0 || upload.IdMarca == 0 ||
        upload.Precio == 0 || upload.Stock == 0 || upload.RutaImagen == null || upload.File == null)
        {
            return BadRequest("No se proporcionó todos los datos necesarios.");
        }

        DynamicParameters dp = new DynamicParameters();

        dp.Add("Nombre", upload.Nombre);
        dp.Add("Descripcion", upload.Descripcion);
        dp.Add("IdMarca", upload.IdMarca);
        dp.Add("IdCategoria", upload.IdCategoria);
        dp.Add("Precio", upload.Precio);
        dp.Add("Stock", upload.Stock);
        dp.Add("RutaImagen", upload.RutaImagen);

        await repository.ExecuteProcedure("crearProducto",dp);
        return Ok(new { file });
    }
    [HttpDelete]
    [Route("ProductoController/Delete")]
    public async Task<BaseResponse> Delete([FromQuery] int id)
    {
        var pr = new DynamicParameters();
        pr.Add("@id_producto",id);
        try
        {
            var rsp = await repository.ExecuteProcedure("eliminarProducto",pr);
            if (rsp == 0)
            {
                return new DataResponse<dynamic>(false, (int)HttpStatusCode.NotFound, "No se encontro el objeto", data: rsp);
            }
            else if (rsp == 1)
            {
                return new DataResponse<dynamic>(true, (int)HttpStatusCode.OK, "Objeto elminiado", data: rsp);
            }
            else
            {
                return new DataResponse<dynamic>(false, (int)HttpStatusCode.InternalServerError, "Se eliminaron multiples entidades.", data: rsp);
            }


        }
        catch (Exception ex)
        {
            return new BaseResponse(false, (int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    [HttpPatch]
    [Route("ProductoController/Patch")]
    public async Task<BaseResponse> Patch([FromBody] Producto dataInput)
    {
        var pr = new DynamicParameters();
        pr.Add("@nombre",dataInput.Nombre);
        pr.Add("@descripion",dataInput.Descripcion);
        pr.Add("@id_categoria",dataInput.IdCategoria);
        pr.Add("@id_marca",dataInput.IdMarca);
        pr.Add("@precio",dataInput.Precio);
        pr.Add("@stock",dataInput.Stock);
        pr.Add("@url_imagen",dataInput.RutaImagen);
        //chequear que falta
        var rsp = await repository.ExecuteProcedure("modificarProducto",pr);
        if (rsp == 0)
        {
            return new DataResponse<dynamic>(false, (int)HttpStatusCode.NotFound, "No se encontro el objeto", data: rsp);
        }
        else if (rsp == 1)
        {
            return new DataResponse<dynamic>(true, (int)HttpStatusCode.OK, "Objeto modificado", data: rsp);
        }
        else
        {
            return new DataResponse<dynamic>(false, (int)HttpStatusCode.InternalServerError, "Se eliminaron multiples entidades.", data: rsp);
        }
    }
}