using cookbook_api.Data;
using Microsoft.AspNetCore.Mvc;

namespace cookbook_api.Repositories;

public class RecipeRepository
{
    private readonly Context _context;
    public RecipeRepository([FromServices] Context context)
    {
        _context = context;
    }

    
}
