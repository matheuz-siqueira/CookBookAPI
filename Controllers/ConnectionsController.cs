using cookbook_api.Dtos.User;
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

    [HttpGet]
    public async Task<ActionResult<List<UserConnectedResponse>>> GetConnections()
    {
        var result = await _service.GetConnection();
        if (result.Any())
        {
            return Ok(result);
        }
        return NoContent();

    }


}
