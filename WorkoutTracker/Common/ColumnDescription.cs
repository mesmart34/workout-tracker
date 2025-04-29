using System.Linq.Expressions;
using WorkoutTracker.Domain.Entities;

namespace WorkoutTracker.Common;

public class ColumnDescription<T> where T : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Caption { get; set; } = null!;
    public Func<object, string>? Template { get; set; }
    public bool Sortable { get; set; } = true;
    public bool Filterable { get; set; } = true;
    public Expression<Func<T, object?>>? Expression { get; set; }
}