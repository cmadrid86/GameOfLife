using DomainObjects.Exceptions;

namespace DomainObjects;

public record Grid
{
    // Universe origin is 0,0

    public int MaxX { get; }
    public int MaxY { get; }
    public HashSet<Cell> Cells { get; }

    private int MaxRegionX { get; set; }
    private int MaxRegionY { get; set; }
    private int MinRegionX { get; set; }
    private int MinRegionY { get; set; }

    public Grid(int maxX, int maxY, IEnumerable<Cell> initialCells)
    {
        MaxX = maxX;
        MaxY = maxY;

        ValidateNodes(initialCells);

        var init = initialCells.Where(cell => cell.IsAlive);
        Cells = new HashSet<Cell>(init);

        var xs = Cells.Select(cell => cell.X);
        var ys = Cells.Select(cell => cell.Y);

        SetRegion(xs.Min(), ys.Min(), xs.Max(), ys.Max());
    }

    public Grid GetNextState()
    {
        var currentRegion = CreateRegion(Cells);
        var newRegion = new List<Cell>();
        
        foreach(var cell in currentRegion)
        {
            var newState = cell.NextState(this);
            newRegion.Add(newState);
        }

        return new Grid(MaxX, MaxY, newRegion);
    }

    private HashSet<Cell> CreateRegion(HashSet<Cell> current)
    {
        var result = new HashSet<Cell>();
        for(var x = MinRegionX; x <= MaxRegionX; x++)
        {
            for (var y = MinRegionY; y <= MaxRegionY; y++)
            {
                var cell = new Cell(x, y, false);
                if (current.TryGetValue(cell, out var currentCell))
                {
                    result.Add(currentCell);
                }
                else
                {
                    result.Add(cell);
                }
            }
        }

        return result;
    }

    private void SetRegion(int minX, int minY, int maxX, int maxY)
    {
        if (minX - 1 >= 0)
        {
            MinRegionX = minX - 1;
        }
        else
        {
            MinRegionX = minX;
        }
        
        if (minY - 1 >= 0)
        {
            MinRegionY = minY - 1;
        }
        else
        {
            MinRegionY = minY;
        }
        
        if (maxX + 1 <= MaxX)
        {
            MaxRegionX = maxX + 1;
        }
        else
        {
            MaxRegionX = maxX;
        }

        if (maxY + 1 <= MaxY)
        {
            MaxRegionY = maxY + 1;
        }
        else
        {
            MaxRegionY = maxY;
        }
    }

    private void ValidateNodes(IEnumerable<Cell> cells)
    {
        if (cells.Any(cell => cell.X > MaxX || cell.Y > MaxY))
        {
            throw new CellOutOfGridException($"A cell is outside the board");
        }
    }
}