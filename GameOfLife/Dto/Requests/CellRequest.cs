using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace GameOfLifeApi.Dto.Requests;

public class CellRequest
{
    [BindRequired]
    [Range(0, int.MaxValue - 1)]
    public int X { get; set; }

    [BindRequired]
    [Range(0, int.MaxValue - 1)]
    public int Y { get; set; }

    [BindRequired]
    public bool IsAlive { get; set; }
}
