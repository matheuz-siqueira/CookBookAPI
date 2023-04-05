namespace cookbook_api.Exceptions;

public class ExistingEmailException : ApplicationException
{
    public ExistingEmailException(string msg) : base (msg)
    {}
}
