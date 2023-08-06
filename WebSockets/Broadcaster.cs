using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;

namespace cookbook_api.WebSockets;

public class Broadcaster
{
    private readonly static Lazy<Broadcaster> _instance = new(() => new Broadcaster());
    public static Broadcaster Instance { get { return _instance.Value; } }
    private ConcurrentDictionary<string, Connection> Dictionary { get; set; }

    public Broadcaster()
    {
        Dictionary = new ConcurrentDictionary<string, Connection>();
    }

    public void InitializeConnection(IHubContext<AddConnection> hubContext, string connectionId)
    {
        var connection = new Connection(hubContext, connectionId);

        Dictionary.TryAdd(connectionId, connection);

        connection.StartTimeCount(CallbackExpired);
    }

    private void CallbackExpired(string connectionId)
    {
        Dictionary.TryRemove(connectionId, out _);
    }

}
