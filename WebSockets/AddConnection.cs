using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace cookbook_api.WebSockets;

[Authorize(Policy = "Logged")]
public class AddConnection : Hub
{
    public async Task GetQRCode()
    {
        var qrCode = "ABCE123";

        await Clients.Caller.SendAsync("QRCodeResult", qrCode);
    }


    public override Task OnConnectedAsync()
    {
        var id = Context.ConnectionId;
        return base.OnConnectedAsync();
    }
}
