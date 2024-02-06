using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace GameOfLifeApi.Dto.Requests;

public class BoardRequest : IValidatableObject
{
    [BindRequired]
    [Range(0, int.MaxValue - 1)]
    public int MaxX { get; set; }

    [BindRequired]
    [Range(0, int.MaxValue - 1)]
    public int MaxY { get; set; }


    [BindRequired]
    public CellRequest[] Cells { get; set; } = Array.Empty<CellRequest>();

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var errors = new List<ValidationResult>();

        for(var i = 0; i < Cells.Length; i++)
        {
            if (Cells[i].X > MaxX)
            {
                errors.Add(new ValidationResult("Point outside of the grid", new[] { $"{nameof(Cells)}[{i}].X" }));
            }
            if (Cells[i].Y > MaxY)
            {
                errors.Add(new ValidationResult("Point outside of the grid", new[] { $"{nameof(Cells)}[{i}].Y" }));
            }
        }

        return errors;
    }
}
