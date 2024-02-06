using Amazon.DynamoDBv2.DataModel;

namespace Repositories.Entities;

internal class BoardEntity
{
    [DynamoDBHashKey("Id")]
    public Guid Id { get; set; }

    public GridEntity Grid { get; set; }

    public int State { get; set; }
}