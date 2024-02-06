namespace Repositories.Entities;

internal class GridEntity
{
    public int MaxX { get; set; }

    public int MaxY { get; set; }

    public CellEntity[]? Cells { get; set; }
}