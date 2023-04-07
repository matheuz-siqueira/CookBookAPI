namespace cookbook_api.Exceptions;

public class IncorretPassword : ApplicationException
{
    public IncorretPassword(string msg) : base (msg)
    {}
}
