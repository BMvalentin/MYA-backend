
using Microsoft.AspNetCore.Mvc;
using MYABackend.Responses;
using System.Net;
using System.Data;

namespace MYABackend.Controllers;

public class CarritoController : ControllerBase
{
    [HttpGet]
    [Route("CarritoController/Get")]
    public  BaseResponse Get()
    {

            return new BaseResponse(false, (int)HttpStatusCode.InternalServerError, "ex.Message");
    }
}