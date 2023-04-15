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

    public Recipe GetById(int recipeId, int userId, bool tracking = false )
    {
        return tracking 
            ? _context.Recipe.Where(r => r.UserId == userId)
                .Include(r => r.Ingredients).FirstOrDefault(r => r.Id == recipeId)
            : _context.Recipe.AsNoTracking().Where(r => r.UserId == userId)
                .Include(r => r.Ingredients).FirstOrDefault(r => r.Id == recipeId); 
    }
}
