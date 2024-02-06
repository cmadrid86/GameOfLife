using AutoMapper;
using DomainObjects;
using GameOfLifeApi.Dto.Requests;
using GameOfLifeApi.Dto.Responses;

namespace GameOfLifeApi.ObjectMaps;

internal class MappingProfile : Profile
{
    public MappingProfile()
    {
        MapApiRequestsToDomain();
        MapDomainToApiResponses();
    }

    private void MapApiRequestsToDomain()
    {
        CreateMap<BoardRequest, Board>()
            .ConstructUsing(request => new Board(new Grid(request.MaxX, request.MaxY, request.Cells.Select(cell => new Cell(cell.X, cell.Y, cell.IsAlive)))))
            .ForAllMembers(opt => opt.Ignore());
    }

    private void MapDomainToApiResponses()
    {
        CreateMap<Board, BoardResponse>()
            .ForMember(response => response.Cells, opt => opt.MapFrom(src => src.Grid!.Cells))
            .ForMember(response => response.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(response => response.State, opt => opt.MapFrom(src => src.State));

        CreateMap<Cell, CellResponse>();
    }
}