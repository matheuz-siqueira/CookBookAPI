using cookbook_api.Dtos.Recipe;
using cookbook_api.Exceptions;
using cookbook_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cookbook_api.Controllers;

[Authorize]
[ApiController]
[Route("recipes")]
public class RecipeController : ControllerBase
{
    private readonly RecipeService _service;

    public RecipeController([FromServices] RecipeService service)
    {
        _service = service; 
    } 
 
    [HttpPost]
    public ActionResult<RecipeResponse> PostRecipe(
        [FromBody] CreateRecipeReq newRecipe)
    {
        return Ok(_service.CreateRecipe(newRecipe, User));
    }

    [HttpGet]
    public ActionResult<List<GetAllResponse>> GetRecipes([FromBody] GetRecipesReq recipes)
    {
       try 
       {
            return Ok(_service.GetRecipes(recipes, User));
       }
       catch(Exception)
       {
            return NoContent();
       }
    }

    [HttpGet("{id:int}")]
    public ActionResult<RecipeResponse> GetRecipe([FromRoute] int id)
    {
        try 
        {
            return Ok(_service.GetById(id, User));
        }
        catch(RecipeNotFound e)
        {
            return NotFound(e.Message);
        }
    }

}
