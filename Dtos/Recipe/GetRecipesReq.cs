using cookbook_api.Models.Enums;

namespace cookbook_api.Dtos.Recipe;

public class GetRecipesReq
{
    public string TitleOrIngredients { get; set; }
    public TypeRecipeEnum? Category { get; set; }
}
