using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Application.Abstractions;
using TodoApp.Domain.Todos;
using TodoApp.Infrastructure.Persistence;

namespace TodoApp.Infrastructure.Repositories;

public sealed class TodoRepository(ApplicationContext db) : ITodoRepository
{
    public Task<TodoItem?> GetByIdAsync(Guid id, CancellationToken ct) =>
        db.Todos.FirstOrDefaultAsync(t => t.Id == id, ct);

    public async Task<(IReadOnlyList<TodoItem> Items, long Total)> SearchAsync(
        int page, int pageSize, string? search, string? sort, bool includeCompleted, CancellationToken ct)
    {
        var query = db.Todos.AsNoTracking( ).Where(t => !t.IsDeleted);

        if (!includeCompleted)
            query = query.Where(t => !t.IsCompleted);

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(t => EF.Functions.ILike(t.Title, $"%{search}%"));

        query = sort switch {
            "created_desc" => query.OrderByDescending(t => t.CreatedUtc),
            "due_asc" => query.OrderBy(t => t.DueUtc),
            "priority" => query.OrderByDescending(t => t.Priority),
            _ => query.OrderBy(t => t.CreatedUtc)
        };

        var total = await query.LongCountAsync(ct);
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(ct);
        return (items, total);
    }

    public async Task AddAsync(TodoItem item, CancellationToken ct)
    {
        await db.Todos.AddAsync(item, ct);
    }

    public Task UpdateAsync(TodoItem item, CancellationToken ct)
    {
        // tracked entity changes are picked up by SaveChanges
        return Task.CompletedTask;
    }
}