using System.Security.Claims;
using System.Security.Principal;
using cookbook_api.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace cookbook_api.WebSockets;

public class LoggedHandler : AuthorizationHandler<LoggedRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserRepository _userRepository;

    public LoggedHandler(IHttpContextAccessor httpContextAccessor,
        UserRepository userRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _userRepository = userRepository;
    }
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context, LoggedRequirement requirement)
    {
        try
        {
            var authorization = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
            if (string.IsNullOrWhiteSpace(authorization))
            {
                context.Fail();
                return;
            }

            var userId = int.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await _userRepository.GetById(userId, false);
            if (user is null)
            {
                context.Fail();
                return;
            }

            context.Succeed(requirement);
        }
        catch
        {
            context.Fail();
        }

    }
}
