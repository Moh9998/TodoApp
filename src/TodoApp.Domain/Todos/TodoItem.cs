
namespace TodoApp.Domain.Todos;

public enum TodoPriority { Low = 0, Medium = 1, High = 2 }

public class TodoItem
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public TodoPriority Priority { get; set; } = TodoPriority.Medium;
    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
    public DateTime? DueUtc { get; set; }
    public bool IsCompleted { get; set; }
    public bool IsDeleted { get; set; }
}
