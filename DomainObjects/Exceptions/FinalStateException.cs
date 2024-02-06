namespace DomainObjects.Exceptions;

public sealed class FinalStateException : CustomException
{
    [NonSerialized]
    public const string DefaultMessage = "The board haven't ended after multiple tries";

    [NonSerialized]
    public const int DefaultStatusCode = 422;

    [NonSerialized]
    public const string DefaultTitle = nameof(FinalStateException);

    public FinalStateException()
        : base(DefaultMessage, DefaultStatusCode, DefaultTitle)
    {
    }

    public FinalStateException(string message)
        : base(message, DefaultStatusCode, DefaultTitle)
    {
    }
}
