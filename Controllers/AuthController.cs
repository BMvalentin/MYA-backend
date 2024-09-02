using Microsoft.AspNetCore.Mvc;
using MYABackend.Responses;
using MYABackend.Repositories;
using MYABackend.Models;
using System.Net;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MYABackend.Controllers;

public class AuthController : ControllerBase
{
    private readonly Repository _repository = new Repository();
    private readonly IConfiguration _configuration; // me costó entenderlo pero basicamente el main instancia solo el IConfiguration y le pasa por constructor todo lo que hay en "appsettings.json", no hace falta instanciar esta clase

    public AuthController(IConfiguration configuration)
    {
        this._configuration = configuration;
    }

    [HttpPost]
    [Route("AuthController/Login")]
    public async Task<BaseResponse> Login([FromBody] Login auth)
    {
        if (auth.Correo != null && auth.Password != null)
        {
            IEnumerable<dynamic> rsp = await _repository.GetListFromProcedure<dynamic>("VerificarCorreo");// Comprobar correo, devuelve contraseña si lo encuentra
            if (rsp != null)
            {
                var clave = auth.HashearPassword();
                UsuarioLogin user = (UsuarioLogin)rsp.FirstOrDefault();

                if (user.Clave == clave)
                {
                    var jwt = GenerationToken(user);
                    return new DataResponse<dynamic>(true, (int)HttpStatusCode.OK, "JWT Creado", data: jwt);
                }
            }
        }
        // Autenticación fallida
        return new BaseResponse(false, (int)HttpStatusCode.InternalServerError, "error");
    }
    private string GenerationToken(UsuarioLogin user){
        string tipo_usuario = user.Tipo_de_usuario == 0 ? "admin" : "user";
        var claims = new[]
        {
            new Claim("tipo_usuario",tipo_usuario),
            new Claim("correo",user.Correo)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],// quien lo crea
            audience: _configuration["Jwt:Audience"], // a quien lo manda
            claims: claims, //una array con los datos del correo
            expires: DateTime.Now.AddMinutes(30), //dice cuanto va a durar el token, en este caso 30 minutos
            signingCredentials: creds //el token en si
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
