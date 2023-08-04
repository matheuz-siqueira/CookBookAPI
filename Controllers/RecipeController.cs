using cookbook_api.Dtos.Recipe;
using cookbook_api.Exceptions;
using cookbook_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cookbook_api.Controllers;

[ApiVersion("1")]
[Authorize]
[ApiController]
[Route("api/v{version:apiVersion}/recipes")]
[Produces("application/json")]
public class RecipeController : ControllerBase
{
    private readonly RecipeService _service;

    public RecipeController([FromServices] RecipeService service)
    {
        _service = service;
    }


    /// <summary>
    /// Cadastrar uma receita
    /// </summary>
    /// <remarks> 
    /// {"title":"string","category":0,"preparation":"string","preparationTime":0,"ingredients":[{"products":"string","quantity":"string"}]}
    /// </remarks> 
    /// <param name="newRecipe">Dados da receita</param>
    /// <returns>Objeto recém criado</returns>
    /// <response code="200">Sucesso</response>
    /// <response code="401">Não autenticado</response>  
    [HttpPost("register-recipe")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<RecipeResponse> PostRecipe(
        [FromBody] CreateUpdateRecipeReq newRecipe)
    {
        return Ok(_service.CreateRecipe(newRecipe, User));
    }

    /// <summary> 
    /// Obter todas as receitas do usuário 
    /// </summary>
    /// <param name="recipes">Parâmetro de pesquisa</param> 
    /// <returns>Coleção de receitas</returns> 
    /// <response code="200">Sucesso</response>
    /// <response code="204">Sucesso</response> 
    /// <response code="401">Não autenticado</response>
    [HttpPost("get-all-recipes")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<List<GetAllResponse>> GetRecipes([FromBody] GetRecipesReq recipes)
    {
        try
        {
            return Ok(_service.GetRecipes(recipes, User));
        }
        catch (Exception)
        {
            return NoContent();
        }
    }

    /// <summary> 
    /// Obter receita por ID
    /// </summary>
    /// <param name="id">ID da receita</param>
    /// <returns>Um evento</returns> 
    /// <response code="200">Sucesso</response>
    /// <response code="404">Não encontrado</response>
    /// <response code="401">Não autenticado</response>
    [HttpGet("get-by-id/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<RecipeResponse> GetRecipe([FromRoute] int id)
    {
        try
        {
            return Ok(_service.GetById(id, User));
        }
        catch (RecipeNotFound e)
        {
            return NotFound(e.Message);
        }
    }


    /// <summary> 
    /// Atualizar uma receita
    /// </summary> 
    /// <remarks>
    /// {"title":"string","category":0,"preparation":"string","preparationTime":0,"ingredients":[{"products":"string","quantity":"string"}]}
    /// </remarks>
    /// <param name="id">ID da receita</param> 
    /// <param name="edited">Dados da receita</param>
    /// <returns>Receita atualizada</returns> 
    /// <response code="200">Sucess</response>
    /// <response code="404">Não encontrado</response> 
    /// <response code="401">Não autenticado</response>
    [HttpPut("update-recipe/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<RecipeResponse> PutRecipe([FromRoute] int id, [FromBody] CreateUpdateRecipeReq edited)
    {
        try
        {
            return Ok(_service.Update(edited, id, User));
        }
        catch (RecipeNotFound e)
        {
            return NotFound(e.Message);
        }
    }


    /// <summary> 
    /// Deletar uma receita
    /// </summary>  
    /// <param name="id">ID da receita</param>
    /// <returns>Nada</returns> 
    /// <response code="204">Sucesso</response>
    /// <response code="404">Não encontrado</response>
    /// <response code="401">Não autenticado</response> 
    [HttpDelete("remove/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult DeleteRecipe([FromRoute] int id)
    {
        try
        {
            _service.Revove(id, User);
            return NoContent();
        }
        catch (RecipeNotFound e)
        {
            return NotFound(e.Message);
        }
    }

}
