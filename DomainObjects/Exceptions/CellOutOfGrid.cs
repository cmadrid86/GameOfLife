namespace DomainObjects.Exceptions;

[Serializable]
public sealed class CellOutOfGridException : CustomException
{
    [NonSerialized]
    public const string DefaultTitle = nameof(CellOutOfGridException);

    [NonSerialized]
    public const int DefaultStatusCode = 400;

    public CellOutOfGridException(string message)
        : base(message, DefaultStatusCode, DefaultTitle)
    {
    }
}