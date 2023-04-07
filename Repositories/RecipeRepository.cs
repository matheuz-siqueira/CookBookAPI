using cookbook_api.Data;
using cookbook_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace cookbook_api.Repositories;

public class RecipeRepository
{
    private readonly Context _context;

    public RecipeRepository([FromServices] Context context)
    {
        _context = context;
    }

    public Recipe CreateRecipe(Recipe newRecipe)
    {
        _context.Recipe.Add(newRecipe);
        _context.SaveChanges();
        return newRecipe;
    }
}
