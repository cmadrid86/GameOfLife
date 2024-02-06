using DomainObjects;

namespace Managers;

public interface IGameManager
{
    Task<Board> GetFinalState(Guid id, int maxAttemps);
    Task<Board> GetNextState(Guid id);
    Task<IEnumerable<Board>> GetNextStates(Guid id, int states);
    Task SaveNewGame(Board board);
}