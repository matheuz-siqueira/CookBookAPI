using cookbook_api.Dtos.User;
using cookbook_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace cookbook_api.WebSockets;

[Authorize(Policy = "Logged")]
public class AddConnection : Hub
{
    private readonly QRCodeService _qrCodeService;
    private readonly Broadcaster _broadcaster;
    private readonly IHubContext<AddConnection> _hubContext;
    public AddConnection(IHubContext<AddConnection> hubContext, QRCodeService qRCodeService)
    {
        _broadcaster = Broadcaster.Instance;
        _qrCodeService = qRCodeService;
        _hubContext = hubContext;
    }
    public async Task GetQRCode()
    {
        (var qrCode, var userId) = await _qrCodeService.GenerateQRCode();

        _broadcaster.InitializeConnection(_hubContext, userId, Context.ConnectionId);

        await Clients.Caller.SendAsync("QRCodeResult", qrCode);
    }

    public async Task QRCodeRead(string codeConnection)
    {
        try
        {
            (var requester, var idCreator) = await _qrCodeService.QRCodeRead(codeConnection);

            var connectionId = _broadcaster.GetConnectionIdCreator(idCreator);

            await Clients.Client(connectionId).SendAsync("QRCodeReadResult", requester);
        }
        catch (Exception e)
        {
            await Clients.Caller.SendAsync("Erro", e.Message);
        }
    }
}
