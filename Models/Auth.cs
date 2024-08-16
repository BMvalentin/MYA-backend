using Microsoft.AspNetCore.Identity;

namespace MYABackend.Models;
public class Auth
{
    public string Password { get; set; }
    public string Correo { get; set; }
    public string HashearPassword()
    {
        var passwordHasher = new PasswordHasher<object>();
        return passwordHasher.HashPassword(null, Password);
    }

}