namespace cookbook_api.Exceptions;

public class InvalidOperation : ApplicationException
{
    public InvalidOperation(string message) : base(message) { }
}
