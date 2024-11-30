using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MYABackend.Repositories;
using MYABackend.Responses;
using MYABackend.Models;
using System.Net;

namespace MYABackend.Controllers;

[ApiController]
public class UsuarioController : ControllerBase
{
    public Repository repository = new Repository();
    [HttpPost]
    [Route("UsuarioController/Create")]
    [AllowAnonymous]
    public async Task<BaseResponse> Create([FromBody] Usuario usuario)
    {
        if (usuario.Apellidos != null && usuario.Apellidos.Length >= 0 && usuario.Nombres != null && usuario.Nombres.Length >= 0
        && usuario.Correo != null && usuario.Correo.Length >= 0 && usuario.Password != null && usuario.Password.Length >= 0)
        {
            try
            {
                var rsp = await repository.ExecuteProcedure("CrearUsuario", usuario.crearUsuario());
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