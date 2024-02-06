using Amazon.DynamoDBv2.DataModel;
using AutoMapper;
using DomainObjects;
using DomainObjects.ValueObjects;
using Microsoft.Extensions.Options;
using Repositories.Entities;

namespace Repositories;

internal class GameRepository : IGameRepository
{
    private readonly IDynamoDBContext _dbContext;
    private readonly IMapper _mapper;
    private readonly DynamoDBOperationConfig _config;
    
    public GameRepository(IDynamoDBContext dBContext, IMapper mapper, IOptions<Settings> settings)
    {
        _config = new DynamoDBOperationConfig
        {
            OverrideTableName = settings.Value.DynamoDbTableName
        };
        _dbContext = dBContext;
        _mapper = mapper;
    }

    public async Task<Board?> GetBoard(Guid id)
    {
        var entity = await _dbContext.LoadAsync<BoardEntity>(id, _config);

        if (entity == null)
        {
            return null;
        }

        return _mapper.Map<Board>(entity);
    }

    public async Task StoreBoard(Board board)
    {
        var entity = _mapper.Map<BoardEntity>(board);
        await _dbContext.SaveAsync(entity, _config);
    }
}
