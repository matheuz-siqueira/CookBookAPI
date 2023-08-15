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

    public async Task<List<Recipe>> GetAllUsers(List<int> userIds)
    {
        return await _context.Recipe.AsNoTracking()
        .Include(r => r.Ingredients)
        .Where(r => userIds.Contains(r.UserId)).ToListAsync();
    }

    public async Task<Recipe> GetById(int recipeId)
    {
        return await _context.Recipe.AsNoTracking()
                .Include(r => r.Ingredients)
                .FirstOrDefaultAsync(r => r.Id == recipeId);
    }

    public async Task<Recipe> GetByIdTracking(int recipeId, int userId)
    {
        return await _context.Recipe.Include(r => r.Ingredients)
            .Where(r => r.UserId == userId).FirstOrDefaultAsync(r => r.Id == recipeId);
    }

    public async Task<int> RecipeCountAsync(int userId)
    {
        return await _context.Recipe.AsNoTracking()
            .CountAsync(r => r.UserId == userId);
    }
    public void Update()
    {
        _context.SaveChanges();
    }

    public void Remove(Recipe recipe)
    {
        _context.Remove(recipe);
        _context.SaveChanges();
    }
}
