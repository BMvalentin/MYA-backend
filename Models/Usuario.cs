using Dapper;

namespace MYABackend.Models;
public class Usuario
{
    public int IdUsuario { get; set; }
    public string Nombres { get; set; }
    public string Apellidos { get; set; }
    public string Correo { get; set; }
    public string Password { get; set; }

    public DynamicParameters crearUsuario ()
    {
        var dp = new DynamicParameters();
        dp.Add("@nombre", Nombres);
        dp.Add("@apellido", Apellidos);
        dp.Add("@correo", Correo);
        dp.Add("@password", Password);
        return dp;
    }
}