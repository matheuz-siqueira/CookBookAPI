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

    public string Remove(string connectionId, string userId)
    {
        Dictionary.TryGetValue(connectionId, out var objConnection);
        var connection = objConnection as Connection;
        connection.StopTime();
        Dictionary.TryRemove(connectionId, out _);
        Dictionary.TryRemove(userId, out _);
        return connection.UserReaderQRCode();
    }
    public void ResetExpiredTimer(string connectionId)
    {
        Dictionary.TryGetValue(connectionId, out var objConnection);
        var connection = objConnection as Connection;
        connection.ResetTimer();
    }

    public void SetConnectionIdUserReader(string idCreator, string connectionIdReader)
    {
        var connectionIdUserReader = GetConnectionIdCreator(idCreator);
        Dictionary.TryGetValue(connectionIdUserReader, out var objConnection);
        var connection = objConnection as Connection;
        connection.SetConnectionIdUserReader(connectionIdReader);
    }
    private void CallbackExpired(string connectionId)
    {
        Dictionary.TryRemove(connectionId, out _);
    }

}
