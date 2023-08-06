using cookbook_api.Data;
using cookbook_api.Models;
using Microsoft.EntityFrameworkCore;

namespace cookbook_api.Repositories;

public class CodeRepository
{
    private readonly Context _context;
    public CodeRepository(Context context)
    {
        _context = context;
    }

    public async Task<Codes> RetriveCodeAsync(string code)
    {
        return await _context.Codes.AsNoTracking().FirstOrDefaultAsync(c => c.Code == code);
    }

    public async Task RegisterAsync(Codes code)
    {
        var codeDB = await _context.Codes.FirstOrDefaultAsync(c => c.UserId == code.UserId);
        if (codeDB is not null)
        {
            codeDB.Code = code.Code;
            _context.Codes.Update(codeDB);
        }
        else
        {
            await _context.Codes.AddAsync(code);
        }
        await _context.SaveChangesAsync();

    }

    public async Task RemoveAsync(int userId)
    {
        var codes = await _context.Codes.Where(c => c.UserId == userId).ToListAsync();
        if (codes.Any())
        {
            _context.Codes.RemoveRange(codes);
            await _context.SaveChangesAsync();
        }

    }
}
