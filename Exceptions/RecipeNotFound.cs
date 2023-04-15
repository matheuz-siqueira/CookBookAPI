namespace cookbook_api.Exceptions;

public class RecipeNotFound : ApplicationException
{
    public RecipeNotFound(string msg) : base (msg) 
    {}
}
