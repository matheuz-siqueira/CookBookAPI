using System.Collections.Generic;
using System.Globalization;
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

    public RecipeService(
        [FromServices] RecipeRepository repository)
    {
        _repository = repository;
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

    public List<GetAllResponse> GetRecipes(GetRecipesReq recipe, ClaimsPrincipal logged)
    {
        var userId = UserId(logged);
        var recipes = _repository.GetAll(userId);
        recipes = Filter(recipe, recipes);

        if (recipes is null)
        {
            throw new Exception();
        }
        return recipes.Adapt<List<GetAllResponse>>();
    }

    public RecipeResponse GetById(int id, ClaimsPrincipal logged)
    {
        return FindById(id, logged, false).Adapt<RecipeResponse>();
    }

    public RecipeResponse Update(CreateUpdateRecipeReq edited, int recipeId, ClaimsPrincipal logged)
    {
        var recipe = FindById(recipeId, logged, true);
        recipe.UpdateAt = DateTime.Now;
        edited.Adapt(recipe);
        _repository.Update();
        return recipe.Adapt<RecipeResponse>();
    }

    public void Revove(int recipeId, ClaimsPrincipal logged)
    {
        var recipe = FindById(recipeId, logged, true);
        _repository.Remove(recipe);
    }

    private Recipe FindById(int id, ClaimsPrincipal logged, bool tracking = true)
    {
        var userId = UserId(logged);
        var response = _repository.GetById(id, userId, tracking)
            ?? throw new RecipeNotFound("Recipe not found");
        return response;
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
