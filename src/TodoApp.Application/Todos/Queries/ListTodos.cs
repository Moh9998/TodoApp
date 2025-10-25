// Application/Todos/Queries/ListTodos.cs
using MediatR;
using TodoApp.Application.Abstractions;
using TodoApp.Application.Common;
using TodoApp.Application.Todos.DTOs;

namespace TodoApp.Application.Todos.Queries;

public record ListTodosQuery(int Page = 1, int PageSize = 20, string? Search = null, string? Sort = null, bool IncludeCompleted = true)
    : IRequest<PagedResult<TodoDto>>;

public sealed class ListTodosHandler(ITodoRepository repo) : IRequestHandler<ListTodosQuery, PagedResult<TodoDto>>
{
    public async Task<PagedResult<TodoDto>> Handle(ListTodosQuery q, CancellationToken ct)
    {
        var (items, total) = await repo.SearchAsync(q.Page, q.PageSize, q.Search, q.Sort, q.IncludeCompleted, ct);

        var dtos = items.Select(t => new TodoDto(t.Id, t.Title, t.Description, t.Priority, t.IsCompleted, t.CreatedUtc, t.DueUtc))
                        .ToList( );

        return new PagedResult<TodoDto>(dtos, q.Page, q.PageSize, total);
    }
}
