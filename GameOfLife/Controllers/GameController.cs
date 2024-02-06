using AutoMapper;
using DomainObjects;
using GameOfLifeApi.Dto.Requests;
using GameOfLifeApi.Dto.Responses;
using Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace GameOfLife.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController : ControllerBase
{
    private readonly IGameManager _gameManager;
    private readonly IMapper _mapper;

    public GameController(IGameManager gameManager, IMapper mapper)
    {
        _gameManager = gameManager;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<Guid> GetNewBoard([FromBody][BindRequired] BoardRequest request)
    {
        var board = _mapper.Map<Board>(request);
        
        await _gameManager.SaveNewGame(board);

        return board.Id;
    }

    [HttpPatch("{id}/final/{maxAttemps}")]
    public async Task<BoardResponse> GetFinalState([FromRoute][Required] Guid id, [FromRoute][Required] int maxAttemps)
    {
        var board = await _gameManager.GetFinalState(id, maxAttemps);
        return _mapper.Map<BoardResponse>(board)!;
    }

    [HttpPatch("{id}/nextstate")]
    public async Task<BoardResponse> GetNextState([FromRoute][Required] Guid id)
    {
        var board = await _gameManager.GetNextState(id);
        return _mapper.Map<BoardResponse>(board)!;
    }

    [HttpPatch("{id}/next/{states}")]
    public async Task<IEnumerable<BoardResponse>> GetNextStates([FromRoute][Required] Guid id, [FromRoute][Required] int states)
    {
        var boards = await _gameManager.GetNextStates(id, states);
        return _mapper.Map<IEnumerable<BoardResponse>>(boards)!;
    }
}