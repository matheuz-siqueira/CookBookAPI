namespace cookbook_api.Exceptions;

public class CodeNotFound : ApplicationException
{
    public CodeNotFound(string msg) : base(msg)
    { }
}
