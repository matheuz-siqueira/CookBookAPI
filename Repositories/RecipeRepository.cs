using cookbook_api.Data;
using cookbook_api.Models;
using cookbook_api.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    public List<Recipe> GetAll(int userId)
    {
        return _context.Recipe.AsNoTracking()
        .Include(r => r.Ingredients)
        .Where(r => r.UserId == userId).ToList();
    }

    public List<Recipe> FilterByCategory(TypeRecipeEnum category, int userId)
    {   
        return _context.Recipe.AsNoTracking()
        .Include(r => r.Ingredients)
        .Where(r => r.UserId == userId)
        .Where(r => r.Category == category).ToList();
    }
}
