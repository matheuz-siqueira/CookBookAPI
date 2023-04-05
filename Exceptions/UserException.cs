namespace cookbook_api.Exceptions;

public class UserException : ApplicationException
{
    public UserException(string msg) : base (msg)
    {}
}
