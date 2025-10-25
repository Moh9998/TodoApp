
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Application.Todos.Commands;
using TodoApp.Application.Todos.Queries;

namespace TodoApp.WebApi.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v1/todos")]
public sealed class TodosController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> List([FromQuery] ListTodosQuery query, CancellationToken ct)
        => Ok(await mediator.Send(query, ct));

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Guid>> Create(CreateTodoCommand cmd, CancellationToken ct)
        => Ok(await mediator.Send(cmd, ct));

    [HttpPut("{id:guid}")]
    [Authorize]
    public async Task<ActionResult> Update(Guid id, UpdateTodoCommand body, CancellationToken ct)
    {
        if (id != body.Id) return BadRequest();
        await mediator.Send(body, ct);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Policy = "RequireAdmin")]
    public async Task<ActionResult> Delete(Guid id, CancellationToken ct)
    {
        await mediator.Send(new DeleteTodoCommand(id), ct);
        return NoContent();
    }
}
