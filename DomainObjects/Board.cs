namespace DomainObjects;

public class Board
{
    public Guid Id { get; } = Guid.NewGuid();
    public Grid Grid { get; }
    public int State { get; } = 0;

    public Board(Grid grid)
    {
        Grid = grid;
    }

    public Board(Guid id, int state, Grid grid)
    {
        Id = id;
        Grid = grid;
        State = state;
    }

    public Board GetNextState()
    {
        return new Board(Id, State + 1, Grid.GetNextState());
    }

    public bool IsDead() => Grid!.Cells.Count == 0;
}
