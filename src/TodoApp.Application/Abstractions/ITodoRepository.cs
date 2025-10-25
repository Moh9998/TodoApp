using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Domain.Todos;

namespace TodoApp.Application.Abstractions
{
    public interface ITodoRepository
    {
        Task<TodoItem?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<(IReadOnlyList<TodoItem> Items, long Total)> SearchAsync(
            int page, int pageSize, string? search, string? sort, bool includeCompleted, CancellationToken ct);
        Task AddAsync(TodoItem item, CancellationToken ct);
        Task UpdateAsync(TodoItem item, CancellationToken ct);
    }
}
