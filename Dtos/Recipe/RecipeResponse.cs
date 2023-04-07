using cookbook_api.Models.Enums;

namespace cookbook_api.Dtos.Recipe;

public class RecipeResponse
{
    public int Id { get; set; }
    public string Title { get; set; }
    public TypeRecipeEnum Category { get; set; }
    public string Preparation { get; set; }
    public DateTime CreatedAt { get; set; }
}
