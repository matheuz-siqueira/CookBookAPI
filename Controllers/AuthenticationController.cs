using cookbook_api.Dtos.User;
using cookbook_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace cookbook_api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly AuthenticationService _authService;

    public AuthenticationController([FromServices] AuthenticationService service)
    {
        _authService = service;
    } 

    [HttpPost]
    public ActionResult<string> Login([FromBody] LoginReq login)
    {
        try 
        {
            var tokenJWT = _authService.Login(login);
            return Ok(tokenJWT);
        }
        catch(Exception e)
        {
            return NotFound(e.Message);
        }
    }

}
