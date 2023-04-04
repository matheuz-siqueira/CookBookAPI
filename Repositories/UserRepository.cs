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

    public User CreateUser(User newUser)
    {
        _context.Users.Add(newUser);
        _context.SaveChanges();
        return newUser;
    }

    public void RemoveUser(User user)
    {
        _context.Remove(user); 
        _context.SaveChanges();
    }

    public void UpdateUser()
    {
        _context.SaveChanges();
    }
    public User GetUserByEmail(string email)
    {
        return _context.Users.AsNoTracking().FirstOrDefault(user => user.Email.Equals(email)); 
    }

}
