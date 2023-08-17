using cookbook_api.Dtos.User;
using cookbook_api.Exceptions;
using cookbook_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cookbook_api.Controllers;

[ApiVersion("1")]
[Authorize]
[ApiController]
[Route("api/v{version:apiVersion}/connections")]
[Produces("application/json")]
public class ConnectionsController : ControllerBase
{
    private readonly ConnectionsService _service;
    public ConnectionsController(ConnectionsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Obter todas as conexões de um usuário
    /// </summary>
    /// <returns>Lista de conexões</returns>
    /// <response code="200">Sucesso</response>
    /// <response code="204">Sucesso</response>
    [HttpGet("get-connections")]
    public async Task<ActionResult<List<UserConnectedResponse>>> GetConnections()
    {
        var result = await _service.GetConnection();
        if (result.Any())
        {
            return Ok(result);
        }
        return NoContent();
    }

    /// <summary>
    /// Remover uma conexão
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Nada</returns>
    /// <response code="204">Sucesso</response> 
    /// <response code="404">Não encontrado</response>
    [HttpDelete("delete/{id:int}")]
    public async Task<ActionResult> DeleteConnection([FromRoute] int id)
    {
        try
        {
            await _service.DeleteConnection(id);
            return NoContent();
        }
        catch (UserException ex)
        {
            return NotFound(new { message = ex.Message });
        }

    }


}

