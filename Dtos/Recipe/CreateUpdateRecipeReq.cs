using cookbook_api.Models.Enums;

namespace cookbook_api.Dtos.Recipe;

public class CreateUpdateRecipeReq
{   
    public string Title { get; set; }
    public TypeRecipeEnum Category { get; set; }
    public string Preparation { get; set; }
    public List<Ingredients> Ingredients { get; set; }
}

public class Ingredients
{
    public string Products { get; set; }
    public string Quantity { get; set; }
}
