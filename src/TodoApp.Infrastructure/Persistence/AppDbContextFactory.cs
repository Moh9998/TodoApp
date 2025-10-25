
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TodoApp.Infrastructure.Persistence;

public sealed class AppDbContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
{
    public ApplicationContext CreateDbContext(string[] args)
    {
        var conn = Environment.GetEnvironmentVariable("DB_CONN")
                  ?? "Host=localhost;Port=5432;Database=todo_db;Username=postgres;Password=123";
        var builder = new DbContextOptionsBuilder<ApplicationContext>( );
        builder.UseNpgsql(conn);
        return new ApplicationContext(builder.Options);
    }
}
