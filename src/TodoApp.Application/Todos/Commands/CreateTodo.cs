// Application/Todos/Commands/CreateTodo.cs
using FluentValidation;
using MediatR;
using TodoApp.Application.Abstractions;
using TodoApp.Domain.Todos;

namespace TodoApp.Application.Todos.Commands;

public record CreateTodoCommand(string Title, string? Description, TodoPriority Priority, DateTime? DueUtc) : IRequest<Guid>;

public sealed class CreateTodoValidator : AbstractValidator<CreateTodoCommand>
{
    public CreateTodoValidator()
    {
        RuleFor(x => x.Title).NotEmpty( ).MaximumLength(200);
    }
}

public sealed class CreateTodoHandler(ITodoRepository repo, IUnitOfWork uow) : IRequestHandler<CreateTodoCommand, Guid>
{
    public async Task<Guid> Handle(CreateTodoCommand c, CancellationToken ct)
    {
        var todo = new TodoItem {
            Id = Guid.NewGuid( ),
            Title = c.Title.Trim( ),
            Description = c.Description?.Trim( ),
            Priority = c.Priority,
            DueUtc = c.DueUtc
        };

        await repo.AddAsync(todo, ct);
        await uow.SaveChangesAsync(ct);
        return todo.Id;
    }
}
