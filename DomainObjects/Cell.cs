using DomainObjects.Exceptions;

namespace DomainObjects;

public record Cell : IEquatable<Cell>
{
    public int X { get; private init; }
    public int Y { get; private init; }
    public bool IsAlive { get; private init; }

    public Cell(int x, int y, bool isAlive)
    {
        if (x < 0)
        {
            throw new CellOutOfGridException("Coordinate X must not be lower than 0");
        }
        if (y < 0)
        {
            throw new CellOutOfGridException("Coordinate Y must not be lower than 0");
        }

        X = x;
        Y = y;
        IsAlive = isAlive;
    }

    public virtual bool Equals(Cell? other)
    {
        if (other is null)
        {
            return false;
        }
        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return X == other.X && Y == other.Y;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public Cell NextState(Grid current)
    {
        var aliveNeighbours = GetAliveNeighbours(current);

        if (!IsAlive)
        {
            if (aliveNeighbours == 3)
            {
                return this with { IsAlive = true };
            }
            return this;
        }

        if (aliveNeighbours < 2)
        {
            return this with { IsAlive = false };
        }
        if (aliveNeighbours < 4)
        {
            return this;
        }
        
        return this with { IsAlive = false };
    }

    private int GetAliveNeighbours(Grid current)
    {
        var alives = 0;
        var xLess1 = X - 1;
        var xPlus1 = X + 1;
        var yLess1 = Y - 1;
        var yPlus1 = Y + 1;

        if (xLess1 >= 0)
        {
            alives += current.Cells.Contains(new Cell(xLess1, Y, true)) ? 1 : 0;

            if (yPlus1 <= current.MaxY)
            {
                alives += current.Cells.Contains(new Cell(xLess1, yPlus1, true)) ? 1 : 0;
            }
            if (yLess1 >= 0)
            {
                alives += current.Cells.Contains(new Cell(xLess1, yLess1, true)) ? 1 : 0;
            }
        }

        if (xPlus1 <= current.MaxX)
        {
            alives += current.Cells.Contains(new Cell(xPlus1, Y, true)) ? 1 : 0;

            if (yPlus1 <= current.MaxY)
            {
                alives += current.Cells.Contains(new Cell(xPlus1, yPlus1, true)) ? 1 : 0;
            }
            if (yLess1 >= 0)
            {
                alives += current.Cells.Contains(new Cell(xPlus1, yLess1, true)) ? 1 : 0;
            }
        }

        if (yLess1 >= 0)
        {
            alives += current.Cells.Contains(new Cell(X, yLess1, true)) ? 1 : 0;
        }
        if (yPlus1 <= current.MaxY)
        {
            alives += current.Cells.Contains(new Cell(X, yPlus1, true)) ? 1 : 0;
        }

        return alives;
    }
}