// Application/Todos/Commands/DeleteTodo.cs
using MediatR;
using TodoApp.Application.Abstractions;

namespace TodoApp.Application.Todos.Commands;

public record DeleteTodoCommand(Guid Id) : IRequest;

public sealed class DeleteTodoHandler(ITodoRepository repo, IUnitOfWork uow) : IRequestHandler<DeleteTodoCommand>
{
    public async Task Handle(DeleteTodoCommand c, CancellationToken ct)
    {
        var todo = await repo.GetByIdAsync(c.Id, ct) ?? throw new KeyNotFoundException("Todo not found");
        todo.IsDeleted = true;
        await repo.UpdateAsync(todo, ct);
        await uow.SaveChangesAsync(ct);
    }
}
