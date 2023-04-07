using cookbook_api.Models;
using Microsoft.EntityFrameworkCore;

namespace cookbook_api.Data;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options)
    {}

    public DbSet<User> Users { get; set; }
    public DbSet<Recipe> Recipe { get; set; }
    public DbSet<Ingredients> Ingredients { get; set; } 
}
