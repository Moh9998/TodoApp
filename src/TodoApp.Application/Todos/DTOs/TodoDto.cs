
using TodoApp.Domain.Todos;

namespace TodoApp.Application.Todos.DTOs;

public record TodoDto(Guid Id, string Title, string? Description, TodoPriority Priority, bool IsCompleted, DateTime CreatedUtc, DateTime? DueUtc);
