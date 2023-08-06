using System.Security.Claims;
using cookbook_api.Models;
using cookbook_api.Repositories;

namespace cookbook_api.Services;

public class QRCodeService
{
    private readonly CodeRepository _codeRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public QRCodeService(CodeRepository codeRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        _codeRepository = codeRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<string> GenerateQRCode()
    {
        var userId = int.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
        var code = new Codes
        {
            Code = Guid.NewGuid().ToString(),
            UserId = userId,
        };
        await _codeRepository.RegisterAsync(code);
        return code.Code;
    }
}
