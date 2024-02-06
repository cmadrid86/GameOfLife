namespace GameOfLifeApi.Dto.Responses;

public class CellResponse
{
    public int X { get; set; }
    public int Y { get; set; }
    public bool IsAlive { get; set; }
}