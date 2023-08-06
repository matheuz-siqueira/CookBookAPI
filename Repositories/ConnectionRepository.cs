using cookbook_api.Data;
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


}
