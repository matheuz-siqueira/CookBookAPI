using cookbook_api.Data;
using cookbook_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cookbook_api.Repositories;

public class UserRepository
{
    private readonly Context _context;

    public UserRepository([FromServices] Context context)
    {
        _context = context;
    }

    public void CreateUser(User newUser)
    {
        _context.Users.Add(newUser);
        _context.SaveChanges();
    }

    public void UpdatePassword()
    {
        _context.SaveChanges();
    }

    public async Task<User> GetById(int id, bool tracking = true)
    {
        return tracking
            ? await _context.Users.FirstOrDefaultAsync(user => user.Id == id)
            : await _context.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Id == id);
    }

    public User GetUserByEmail(string email)
    {
        return _context.Users.AsNoTracking()
        .FirstOrDefault(user => user.Email.Equals(email));
    }

    public User GetProfile(int id)
    {
        return _context.Users.AsNoTracking().FirstOrDefault(u => u.Id == id);
    }

}
