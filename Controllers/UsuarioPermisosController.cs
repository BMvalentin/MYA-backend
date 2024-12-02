using Microsoft.AspNetCore.Mvc;
using MYABackend.Repositories;

namespace MYABackend.Controllers;

[ApiController]
public class UsusarioPermisos : ControllerBase
{
    public Repository repository = new Repository();
    
}