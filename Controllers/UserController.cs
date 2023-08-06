using cookbook_api.Dtos.User;
using cookbook_api.Exceptions;
using cookbook_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cookbook_api.Controllers;

[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/users")]
[Produces("application/json")]
public class UserController : ControllerBase
{
    private readonly UserService _service;

    public UserController([FromServices] UserService service)
    {
        _service = service;
    }

    /// <summary> 
    /// Cadastrar usuário 
    /// </summary> 
    /// <remarks> 
    /// {"name":"string","email":"validstring","password":"string"}
    /// </remarks>
    /// <param name="newUser">Dados do usuário</param>
    /// <returns>Token de autenticação</returns> 
    /// <response code="200">Sucesso</response>
    /// <response code="400">Email já cadastrado</response>  
    [HttpPost("create-account")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<TokenResponse> PostUser([FromBody] CreateUserReq newUser)
    {
        try
        {
            var tokenJWT = _service.CreateUser(newUser);
            return Ok(tokenJWT);
        }
        catch (ExistingEmailException e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary> 
    /// Obter informações do usuário logado
    /// </summary> 
    /// <returns>Dados do usuário logado</returns>
    /// <response code="200">Sucesso</response> 
    /// <response code="401">Não autenticado</response> 
    [Authorize]
    [HttpGet("getprofile")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<GetProfileResponse> GetUser()
    {
        return Ok(_service.GetProfile(User));
    }

    /// <summary> 
    /// Atualizar senha do usuário logado 
    /// </summary> 
    /// <remarks> 
    /// {"currentPassword":"string","newPassword":"string"}
    /// </remarks> 
    /// <param name="update">Objeto para alterar senha</param>
    /// <returns>Nada</returns> 
    /// <response code="204">Sucesso</response> 
    /// <response code="400">Dados incorretos</response>
    /// <response code="401">Não autenticado</response>  
    [Authorize]
    [HttpPut("update-password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> PutUserUpdatePassword([FromBody] UpdatePasswordReq update)
    {
        try
        {
            await _service.UpdatePassword(update, User);
            return NoContent();
        }
        catch (IncorretPassword e)
        {
            return BadRequest(e.Message);
        }
    }
}
