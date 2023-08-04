using cookbook_api.Dtos.User;
using cookbook_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace cookbook_api.Controllers;

[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/authentication")]
[Produces("application/json")]
public class AuthenticationController : ControllerBase
{
    private readonly AuthenticationService _authService;

    public AuthenticationController([FromServices] AuthenticationService service)
    {
        _authService = service;
    }

    /// <summary> 
    /// Logar no sistema
    /// </summary> 
    /// <remarks> 
    /// {"email":"validstring","password":"string"}
    /// </remarks> 
    /// <param name="login">Dados de login</param> 
    /// <returns>Token de acesso</returns> 
    /// <response code="200">Sucesso</response> 
    /// <response code="400">Dados inválidos</response>  
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<TokenResponse> Login([FromBody] LoginReq login)
    {
        try
        {
            var tokenJWT = _authService.Login(login);
            return Ok(tokenJWT);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
}
