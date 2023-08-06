using cookbook_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace cookbook_api.WebSockets;

[Authorize(Policy = "Logged")]
public class AddConnection : Hub
{
    private readonly QRCodeService _qrCodeService;
    public AddConnection(QRCodeService qRCodeService)
    {
        _qrCodeService = qRCodeService;
    }
    public async Task GetQRCode()
    {
        var qrCode = await _qrCodeService.GenerateQRCode();

        await Clients.Caller.SendAsync("QRCodeResult", qrCode);
    }
}
