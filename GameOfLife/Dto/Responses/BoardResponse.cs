namespace GameOfLifeApi.Dto.Responses;

public class BoardResponse
{
    public Guid Id { get; set; }
    public int State { get; set; }
    public IEnumerable<CellResponse> Cells { get; set; } = Enumerable.Empty<CellResponse>();
}