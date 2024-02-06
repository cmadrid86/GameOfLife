using DomainObjects;

namespace Repositories;

public interface IGameRepository
{
    public Task<Board?> GetBoard(Guid id);
    public Task StoreBoard(Board board);
}
