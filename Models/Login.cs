using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MYABackend.Models;
public class Login
{
    [Required(ErrorMessage = "El correo es requerido.")]
    public string Correo { get; set; }
    
    [Required(ErrorMessage = "La contraseña es requerida.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    public string HashearPassword()
    {
        var passwordHasher = new PasswordHasher<object>();
        return passwordHasher.HashPassword(null, Password);
    }

}