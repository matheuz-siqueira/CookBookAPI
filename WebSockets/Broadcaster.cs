using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;

namespace cookbook_api.WebSockets;

public class Broadcaster
{
    private readonly static Lazy<Broadcaster> _instance = new(() => new Broadcaster());
    public static Broadcaster Instance { get { return _instance.Value; } }
    private ConcurrentDictionary<string, object> Dictionary { get; set; }

    public Broadcaster()
    {
        Dictionary = new ConcurrentDictionary<string, object>();
    }

    public void InitializeConnection(IHubContext<AddConnection> hubContext,
        string idCreator, string connectionId)
    {
        var connection = new Connection(hubContext, connectionId);

        Dictionary.TryAdd(connectionId, connection);
        Dictionary.TryAdd(idCreator, connectionId);

        connection.StartTimeCount(CallbackExpired);
    }

    public string GetConnectionIdCreator(string userId)
    {
        if (!Dictionary.TryGetValue(userId, out var connectionId))
        {
            throw new Exception("");
        }
        return connectionId.ToString();
    }

    private void CallbackExpired(string connectionId)
    {
        Dictionary.TryRemove(connectionId, out _);
    }

}
