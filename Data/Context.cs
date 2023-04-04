using Microsoft.EntityFrameworkCore;

namespace cookbook_api.Data;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options)
    {}
}
