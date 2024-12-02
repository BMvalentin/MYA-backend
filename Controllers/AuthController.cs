using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using MYABackend.Repositories;
using MYABackend.Responses;
using System.Security.Claims;
using MYABackend.Models;
using System.Text;
using System.Net;
using Microsoft.AspNetCore.Identity;

namespace MYABackend.Controllers;

[ApiController]
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
    [AllowAnonymous]
    public async Task<BaseResponse> Login([FromBody] Login auth)
    {
        if (auth.Correo != null && auth.Password != null)
        {
            List<UsuarioLogin> rsp = await _repository.GetListFromProcedure<UsuarioLogin>("verificarCorreo", auth.verificarCorreo());
            if (rsp != null)
            {
                UsuarioLogin user = rsp.FirstOrDefault();

                var password = new PasswordHasher<object>();
                var result = password.VerifyHashedPassword(null, user.clave, auth.Password);

                if (result == PasswordVerificationResult.Success)
                {
                    string jwt = GenerationToken(user);
                    return new DataResponse<dynamic>(true, (int)HttpStatusCode.OK, "JWT Creado", data: jwt);
                }
            }
        }
        return new BaseResponse(false, (int)HttpStatusCode.InternalServerError, "error");
    }

    private string GenerationToken(UsuarioLogin user)
    {
        var claims = new[]
        {
            new Claim("tipo_usuario",user.tipo_de_usuario)
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
