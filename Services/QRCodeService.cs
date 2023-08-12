using System.Security.Claims;
using cookbook_api.Dtos.User;
using cookbook_api.Exceptions;
using cookbook_api.Models;
using cookbook_api.Repositories;
using HashidsNet;

namespace cookbook_api.Services;

public class QRCodeService
{
    private readonly CodeRepository _codeRepository;
    private readonly IHashids _hashids;
    private readonly UserRepository _userRepository;
    private readonly ConnectionRepository _connectionRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public QRCodeService(CodeRepository codeRepository,
        IHttpContextAccessor httpContextAccessor,
        UserRepository userRepository,
        ConnectionRepository connectionRepository,
        IHashids hashids)
    {
        _codeRepository = codeRepository;
        _httpContextAccessor = httpContextAccessor;
        _userRepository = userRepository;
        _connectionRepository = connectionRepository;
        _hashids = hashids;
    }

    public async Task<(string qrCode, string userId)> GenerateQRCode()
    {
        var userId = int.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
        var code = new Codes
        {
            Code = Guid.NewGuid().ToString(),
            UserId = userId,
        };
        await _codeRepository.RegisterAsync(code);
        return (code.Code, _hashids.Encode(userId));
    }

    public async Task<(UserConnectionResponse requester, string idCreator)>
        QRCodeRead(string codeConnection)
    {
        var userId = int.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
        var logged = await _userRepository.GetById(userId, false);
        var code = await _codeRepository.RetriveCodeAsync(codeConnection);
        await Validate(code, logged);

        var requester = new UserConnectionResponse
        {
            Id = _hashids.Encode(userId),
            Name = logged.Name
        };

        return (requester, _hashids.Encode(code.UserId));

    }

    public async Task<string> ConnectionAccepted(string idUserToConnect)
    {
        var userId = int.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
        await _codeRepository.RemoveAsync(userId);

        await _connectionRepository.RegisterAsync(new Connection
        {
            UserId = userId,
            ConnectedWithUserId = _hashids.DecodeSingle(idUserToConnect)
        });

        await _connectionRepository.RegisterAsync(new Connection
        {
            UserId = _hashids.DecodeSingle(idUserToConnect),
            ConnectedWithUserId = userId
        });

        return _hashids.Encode(userId);

    }


    public async Task<string> ConnectionRefused()
    {
        var userId = int.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
        await _codeRepository.RemoveAsync(userId);
        return _hashids.Encode(userId);
    }

    private async Task Validate(Codes code, User logged)
    {
        if (code is null)
        {
            throw new CodeNotFound("code not found");
        }
        if (code.UserId == logged.Id)
        {
            throw new InvalidOperation("invalid operation");
        }

        var existing = await _connectionRepository.ExistingConnection(code.UserId, logged.Id);
        if (existing)
        {
            throw new InvalidOperation("invalid operation");
        }
    }


}
