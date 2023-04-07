namespace cookbook_api.Dtos.Recipe;

public class CreateRecipeReq
{   
    public string Title { get; set; }
    public string Category { get; set; }
    public string Preparation { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<Ingredients> Ingredients { get; set; }
}

public class Ingredients
{
    public string Products { get; set; }
    public string Quantity { get; set; }
}
