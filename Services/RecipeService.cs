using System.Security.Claims;
using cookbook_api.Dtos.Recipe;
using cookbook_api.Exceptions;
using cookbook_api.Models;
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
        newRecipe.CreatedAt = DateTime.Now; 
        var recipe = newRecipe.Adapt<Recipe>();
        _repository.CreateRecipe(recipe); 
        return recipe.Adapt<RecipeResponse>(); 
    }



}
