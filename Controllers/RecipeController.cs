using cookbook_api.Dtos.Recipe;
using cookbook_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cookbook_api.Controllers;

[ApiController]
[Route("recipes")]
public class RecipeController : ControllerBase
{
    private readonly RecipeService _service;

    public RecipeController([FromServices] RecipeService service)
    {
        _service = service; 
    } 

    [Authorize] 
    [HttpPost]
    public ActionResult<RecipeResponse> PostRecipe(
        [FromBody] CreateRecipeReq newRecipe)
    {
        return Ok(_service.CreateRecipe(newRecipe, User));
    }

}
