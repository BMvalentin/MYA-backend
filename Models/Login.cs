using System.ComponentModel.DataAnnotations;
using Dapper;

namespace MYABackend.Models;
public class Login
{
    [Required(ErrorMessage = "El correo es requerido.")]
    public string Correo { get; set; }
    
    [Required(ErrorMessage = "La contraseña es requerida.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    public DynamicParameters verificarCorreo()
    {
        var dp = new DynamicParameters();
        dp.Add("@correo", Correo);
        return dp;
    }

}