// tests/TodoApp.UnitTests/TodoHandlersTests.cs
using FluentAssertions;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using TodoApp.Application.Abstractions;
using TodoApp.Application.Todos.Commands;
using TodoApp.Domain.Todos;
using Xunit;

namespace TodoApp.UnitTests;

public class TodoHandlersTests
{
    [Fact]
    public async Task CreateTodo_should_call_repo_and_uow()
    {
        var repo = new Mock<ITodoRepository>( );
        var uow = new Mock<IUnitOfWork>( );

        var handler = new CreateTodoHandler(repo.Object, uow.Object);

        var id = await handler.Handle(new CreateTodoCommand("Test", "Desc", TodoPriority.Medium, null), default);

        id.Should( ).NotBeEmpty( );
        repo.Verify(r => r.AddAsync(It.IsAny<TodoItem>( ), It.IsAny<CancellationToken>( )), Times.Once);
        uow.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>( )), Times.Once);
    }
}
