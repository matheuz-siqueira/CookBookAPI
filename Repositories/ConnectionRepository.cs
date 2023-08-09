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

}
