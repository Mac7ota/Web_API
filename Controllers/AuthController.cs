

using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/v1/Auth")]
public class AuthController : Controller
{
    [HttpPost]
    public IActionResult Auth(string username, string password)
    {
        if (username == "admin" && password == "admin")
        {
            var token = TokenService.GenerateToken(new Models.Employee());
            return Ok(token);
        }

        return BadRequest("username or password is incorrect");
    }
}