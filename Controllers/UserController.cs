using cookbook_api.Dtos.User;
using cookbook_api.Exceptions;
using cookbook_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace cookbook_api.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private readonly UserService _service;

    public UserController([FromServices] UserService service)
    {
        _service = service;
    }

    [HttpPost]
    public ActionResult PostUser([FromBody] CreateUserReq newUser)
    {
        try
        {
            _service.CreateUser(newUser); 
            return Ok();
        }
        catch(ExistingEmailException e)
        {
            return BadRequest(e.Message);
        }
    }
}
