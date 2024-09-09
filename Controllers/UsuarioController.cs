
using Microsoft.AspNetCore.Mvc;
using MYABackend.Responses;
using System.Net;
using System.Data;
using System.Xml.Serialization;

using MYABackend.Models;
using Dapper;
using MYABackend.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
namespace MYABackend.Controllers;

[ApiController]
public class UsuarioController : ControllerBase
{
    public Repository repository = new Repository();
    [HttpPost]
    [Route("UsuarioController/Create")]
    public async Task<BaseResponse> Create([FromBody] Usuario usuario)
    {
        if (usuario.Apellidos != null && usuario.Apellidos.Length >= 0 && usuario.Nombres != null && usuario.Nombres.Length >= 0
        && usuario.Correo != null && usuario.Correo.Length >= 0 && usuario.Password != null && usuario.Password.Length >= 0)
        {
            var dp = new DynamicParameters();
            dp.Add("@nombre", usuario.Nombres);
            dp.Add("@apellido", usuario.Apellidos);
            dp.Add("@correo", usuario.Correo);
            dp.Add("@password", usuario.Password);
            try
            {
                var rsp = await repository.ExecuteProcedure("CrearUsuario", dp);
                return new DataResponse<dynamic>(true, (int)HttpStatusCode.OK, "Entidad Creada", data: rsp);
            }
            catch (Exception e)
            {
                return new BaseResponse(false, (int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
        return new BaseResponse(false, (int)HttpStatusCode.InternalServerError, "Erros al cargar usuario");
    }
}