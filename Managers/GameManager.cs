using DomainObjects;
using DomainObjects.Exceptions;
using Repositories;

namespace Managers;

internal class GameManager : IGameManager
{
    private readonly IGameRepository _gameRepository;

    public GameManager(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<Board> GetFinalState(Guid id, int maxAttemps)
    {
        var board = await GetBoard(id);
        for (var attemp = 0; attemp < maxAttemps && !board.IsDead(); attemp++)
        {
            board = board.GetNextState();
        }

        await _gameRepository.StoreBoard(board);

        if (board.IsDead())
        {
            return board;
        }

        throw new FinalStateException();
    }

    public async Task<Board> GetNextState(Guid id)
    {
        var board = await GetBoard(id);
        board = board.GetNextState();

        await _gameRepository.StoreBoard(board);

        return board;
    }

    public async Task<IEnumerable<Board>> GetNextStates(Guid id, int states)
    {
        var result = new List<Board>();
        var board = await GetBoard(id);

        for (var attemp = 0; attemp < states; attemp++)
        {
            board = board.GetNextState();
            result.Add(board);
        }

        await _gameRepository.StoreBoard(board);

        return result;
    }

    public Task SaveNewGame(Board board)
    {
        return _gameRepository.StoreBoard(board);
    }

    private async Task<Board> GetBoard(Guid id)
    {
        var board = await _gameRepository.GetBoard(id);
        return board ?? throw new BoardNotFoundException();
    }
}
