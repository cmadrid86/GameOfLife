namespace DomainObjects.Exceptions;

[Serializable]
public sealed class BoardNotFoundException : CustomException
{
    [NonSerialized]
    public const string DefaultTitle = nameof(BoardNotFoundException);

    [NonSerialized]
    public const string DefaultMessage = "The board id was not found";

    [NonSerialized]
    public const int DefaultStatusCode = 404;

    public BoardNotFoundException() 
        : base(DefaultMessage, DefaultStatusCode, DefaultTitle)
    {
    }
}
