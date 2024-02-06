using AutoMapper;

namespace Repositories.ObjectMaps;

internal class MappingProfile : Profile
{
    public MappingProfile()
    {
        MapDomainToEntities();
        MapEntitiesToDomain();
    }

    private void MapDomainToEntities()
    {
        CreateMap<DomainObjects.Cell, Entities.CellEntity>();

        CreateMap<DomainObjects.Grid, Entities.GridEntity>();

        CreateMap<DomainObjects.Board, Entities.BoardEntity>();
    }

    private void MapEntitiesToDomain()
    {
        CreateMap<Entities.CellEntity, DomainObjects.Cell>()
            .ConstructUsing(e => new DomainObjects.Cell(e.X, e.Y, e.IsAlive))
            .ForAllMembers(opt => opt.Ignore());

        CreateMap<Entities.CellEntity, DomainObjects.Cell>()
            .ConstructUsing(e => new DomainObjects.Cell(e.X, e.Y, e.IsAlive))
            .ForAllMembers(opt => opt.Ignore());

        CreateMap<Entities.BoardEntity, DomainObjects.Board>()
            .ConstructUsing(e => new DomainObjects.Board(
                e.Id,
                e.State,
                new DomainObjects.Grid(
                    e.Grid.MaxX,
                    e.Grid.MaxY,
                    e.Grid.Cells!.Select(c => new DomainObjects.Cell(
                        c.X,
                        c.Y,
                        c.IsAlive)))))
            .ForAllMembers(opt => opt.Ignore());
    }
}
