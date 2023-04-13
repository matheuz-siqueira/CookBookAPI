using System.Collections.Generic;
using System.Security.Claims;
using cookbook_api.Dtos.Recipe;
using cookbook_api.Exceptions;
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
    private readonly AuthenticationService _authService;
    private readonly UserRepository _userRepository;

    public RecipeService(
        [FromServices] RecipeRepository repository,
        [FromServices] AuthenticationService authService,
        [FromServices] UserRepository userRepository)
    {  
        _repository = repository;
        _authService = authService;
        _userRepository = userRepository;
    }

    public RecipeResponse CreateRecipe(CreateRecipeReq newRecipe, ClaimsPrincipal logged)
    {
        var id = int.Parse(logged.FindFirstValue(ClaimTypes.NameIdentifier)); 
        var recipe = newRecipe.Adapt<Recipe>(); 
        recipe.CreatedAt = DateTime.Now;
        recipe.UserId = id;
        _repository.CreateRecipe(recipe); 
        return recipe.Adapt<RecipeResponse>(); 
    }

    public List<GetAllResponse> GetRecipes(GetRecipesReq recipe, ClaimsPrincipal logged)
    {   
        var userId = int.Parse(logged.FindFirstValue(ClaimTypes.NameIdentifier));

        return (recipe.Category.HasValue) 
            ? Filter((TypeRecipeEnum)recipe.Category, userId).Adapt<List<GetAllResponse>>()
            : _repository.GetAll(userId).Adapt<List<GetAllResponse>>();     
    }

    private int UserId(ClaimsPrincipal logged)
    {
        return int.Parse(logged.FindFirstValue(ClaimTypes.NameIdentifier));
    }

    private List<Recipe> Filter(TypeRecipeEnum category, int userId)
    {
        return _repository.FilterByCategory(category, userId);
    }

}
