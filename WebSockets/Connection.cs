using System.Timers;
using Microsoft.AspNetCore.SignalR;

namespace cookbook_api.WebSockets;

public class Connection
{
    private short SecondTime { get; set; }
    private System.Timers.Timer Timer { get; set; }
    private readonly IHubContext<AddConnection> _hubContext;
    private readonly string Creator;
    private Action<string> _callbackExpired;
    private string ConnectionIdUserReader;

    public Connection(IHubContext<AddConnection> hubContext, string creator)
    {
        _hubContext = hubContext;
        Creator = creator;
    }
    public void StartTimeCount(Action<string> callbackExpired)
    {
        _callbackExpired = callbackExpired;
        StartTimer();
    }

    public void ResetTimer()
    {
        StopTime();
        StartTimer();
    }

    public void StopTime()
    {
        Timer?.Stop();
        Timer?.Dispose();
        Timer = null;
    }

    public void SetConnectionIdUserReader(string connectionId)
    {
        ConnectionIdUserReader = connectionId;
    }
    public string UserReaderQRCode()
    {
        return ConnectionIdUserReader;
    }

    private void StartTimer()
    {
        SecondTime = 60;
        Timer = new System.Timers.Timer(1000)
        {
            Enabled = false
        };
        Timer.Elapsed += ElapsedTimerAsync;
        Timer.Enabled = true;
    }

    private async void ElapsedTimerAsync(object sender, ElapsedEventArgs e)
    {
        if (SecondTime >= 0)
            await _hubContext.Clients.Client(Creator).SendAsync("SetTimeLeft", SecondTime--);
        else
        {
            StopTime();
            _callbackExpired(Creator);
        }

    }
}
