using System.Net.Http.Headers;
using cookbook_api.Data;
using cookbook_api.Models;
using Microsoft.EntityFrameworkCore;

namespace cookbook_api.Repositories;

public class ConnectionRepository
{
    private readonly Context _context;
    public ConnectionRepository(Context context)
    {
        _context = context;
    }

    public async Task<bool> ExistingConnection(int idUserA, int idUserB)
    {
        return await _context.Connections.AsNoTracking().AnyAsync(
            c => c.UserId == idUserA && c.ConnectedWithUserId == idUserB);
    }

    public async Task RegisterAsync(Connection connection)
    {
        await _context.Connections.AddAsync(connection);
        await _context.SaveChangesAsync();
    }
    public async Task<List<int>> GetConnectionsAsync(int userId)
    {
        return await _context.Connections.AsNoTracking()
        .Where(c => c.UserId == userId)
        .Select(c => c.ConnectedWithUserId)
        .ToListAsync();
    }
    public async Task RemoveConnectionAsync(int loggedUserId, int idToRemove)
    {
        var connections = await _context.Connections
            .Where(c => (c.UserId == loggedUserId && c.ConnectedWithUserId == idToRemove)
            || (c.ConnectedWithUserId == loggedUserId && c.UserId == idToRemove))
            .ToListAsync();

        _context.Connections.RemoveRange(connections);
        await _context.SaveChangesAsync();
    }
}
