using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using cookbook_api.Dtos.Recipe;
using cookbook_api.Exceptions;
using cookbook_api.Extensions;
using cookbook_api.Interfaces;
using cookbook_api.Models;
using cookbook_api.Models.Enums;
using cookbook_api.Repositories;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace cookbook_api.Services;

public class RecipeService
{
    private readonly RecipeRepository _repository;
    private readonly ConnectionRepository _connectionRepository;

    public RecipeService(
        RecipeRepository repository,
        ConnectionRepository connectionRepository)
    {
        _repository = repository;
        _connectionRepository = connectionRepository;
    }

    public RecipeResponse CreateRecipe(CreateUpdateRecipeReq newRecipe, ClaimsPrincipal logged)
    {
        var id = UserId(logged);

        if (newRecipe.PreparationTime <= 0 || newRecipe.PreparationTime > 1000)
        {
            throw new Exception("Invalid preparation time");
        }

        var recipe = newRecipe.Adapt<Recipe>();
        var now = DateTime.Now;

        recipe.CreatedAt = now;
        recipe.UpdateAt = now;
        recipe.UserId = id;
        _repository.CreateRecipe(recipe);
        return recipe.Adapt<RecipeResponse>();
    }

    public async Task<List<GetAllResponse>> GetRecipes(GetRecipesReq recipe, ClaimsPrincipal logged)
    {
        var userId = UserId(logged);
        var recipes = _repository.GetAll(userId);
        recipes = Filter(recipe, recipes);
        var connections = await _connectionRepository.GetConnectionsAsync(userId);
        var recipesConnections = await _repository.GetAllUsers(connections);
        recipesConnections = Filter(recipe, recipesConnections);
        recipes = recipes.Concat(recipesConnections).ToList();
        return recipes.Adapt<List<GetAllResponse>>();
    }

    public async Task<RecipeResponse> GetById(int id, ClaimsPrincipal logged)
    {
        var recipe = await _repository.GetById(id);
        var userId = UserId(logged);
        var connections = await _connectionRepository.GetConnectionsAsync(userId);

        if (recipe is null ||
            (recipe.UserId != userId && !connections.Contains(recipe.UserId)))
        {
            throw new RecipeNotFound("Recipe not found");
        }
        var response = recipe;
        return response.Adapt<RecipeResponse>();
    }

    public async Task<RecipeResponse> Update(CreateUpdateRecipeReq edited, int recipeId, ClaimsPrincipal logged)
    {
        var userId = UserId(logged);
        var recipe = await _repository.GetByIdTracking(recipeId, userId);
        recipe.UpdateAt = DateTime.Now;
        edited.Adapt(recipe);
        _repository.Update();
        return recipe.Adapt<RecipeResponse>();
    }

    public async Task Revove(int recipeId, ClaimsPrincipal logged)
    {
        var userId = UserId(logged);
        var recipe = await _repository.GetByIdTracking(recipeId, userId);
        _repository.Remove(recipe);
    }

    private static int UserId(ClaimsPrincipal logged)
    {
        return int.Parse(logged.FindFirstValue(ClaimTypes.NameIdentifier));
    }

    private static List<Recipe> Filter(GetRecipesReq recipe, List<Recipe> recipes)
    {
        var filters = recipes;
        if (recipe.Category.HasValue)
        {
            filters = recipes.Where(r => r.Category == recipe.Category.Value).ToList();
        }
        if (!string.IsNullOrWhiteSpace(recipe.TitleOrIngredients))
        {
            filters = recipes
            .Where(r => r.Title.CompareNoCase(recipe.TitleOrIngredients) || r.Ingredients.Any(i => i.Products.CompareNoCase(recipe.TitleOrIngredients)))
            .ToList();
        }
        return filters.OrderBy(r => r.Title).ToList();
    }

}
