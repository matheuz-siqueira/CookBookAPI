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

    public void UpdatePassword(User user)
    {
        _context.SaveChanges();
    }

    public User GetById(int id, bool tracking = true)
    {
        return tracking 
            ? _context.Users.FirstOrDefault(user => user.Id == id) 
            : _context.Users.AsNoTracking().FirstOrDefault(user => user.Id == id); 
    }
    
    public User GetUserByEmail(string email)
    {
        return _context.Users.AsNoTracking().FirstOrDefault(user => user.Email.Equals(email)); 
    }

}
