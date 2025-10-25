using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Application.Abstractions;
using TodoApp.Application.Todos.DTOs;

namespace TodoApp.Application.Todos.Queries
{
    public record GetOneQuery(Guid Id) : IRequest<TodoDto>;

    public sealed class GetOneQueryHandler : IRequestHandler<GetOneQuery, TodoDto>
    {
        private readonly ITodoRepository _repo;

        public GetOneQueryHandler(ITodoRepository repo)
        {
            _repo = repo;
        }

        public async Task<TodoDto> Handle(GetOneQuery request, CancellationToken cancellationToken)
        {
            var item = await _repo.GetByIdAsync(request.Id, cancellationToken) ??
                throw new Exception("Item Not found");

            return new TodoDto(
                item.Id,
                item.Title,
                item.Description,
                item.Priority,
                item.IsCompleted,
                item.CreatedUtc,
                item.DueUtc
            );
        }
    }
}
