// Application/Todos/Commands/UpdateTodo.cs
using FluentValidation;
using MediatR;
using TodoApp.Application.Abstractions;
using TodoApp.Domain.Todos;

namespace TodoApp.Application.Todos.Commands;

public record UpdateTodoCommand(Guid Id, string Title, string? Description, TodoPriority Priority, bool IsCompleted, DateTime? DueUtc) : IRequest;

public sealed class UpdateTodoValidator : AbstractValidator<UpdateTodoCommand>
{
    public UpdateTodoValidator()
    {
        RuleFor(x => x.Id).NotEmpty( );
        RuleFor(x => x.Title).NotEmpty( ).MaximumLength(200);
    }
}

public sealed class UpdateTodoHandler(ITodoRepository repo, IUnitOfWork uow) : IRequestHandler<UpdateTodoCommand>
{
    public async Task Handle(UpdateTodoCommand c, CancellationToken ct)
    {
        var todo = await repo.GetByIdAsync(c.Id, ct) ?? throw new KeyNotFoundException("Todo not found");

        todo.Title = c.Title.Trim( );
        todo.Description = c.Description?.Trim( );
        todo.Priority = c.Priority;
        todo.IsCompleted = c.IsCompleted;
        todo.DueUtc = c.DueUtc;

        await repo.UpdateAsync(todo, ct);
        await uow.SaveChangesAsync(ct);
    }
}
