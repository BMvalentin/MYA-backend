
using Microsoft.AspNetCore.Mvc;
using MYABackend.Responses;
using SistemasCafeBackEnd.Repositories;
using System.Net;

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
    
}