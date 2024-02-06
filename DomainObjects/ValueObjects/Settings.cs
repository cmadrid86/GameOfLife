namespace DomainObjects.ValueObjects;

public class Settings
{
    public string? DynamoDbAccessKey { get; set; }
    public string? DynamoDbSecretKey { get; set; }
    public string? DynamoDbTableName { get; set; }
    public string? DynamoDbUrl { get; set; }
}
