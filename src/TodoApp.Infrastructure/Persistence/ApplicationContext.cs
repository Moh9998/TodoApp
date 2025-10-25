
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoApp.Domain.Todos;
using TodoApp.Domain.Users;

namespace TodoApp.Infrastructure.Persistence;

public class ApplicationContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public DbSet<TodoItem> Todos => Set<TodoItem>();

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder b)
    {
        base.OnModelCreating(b);

        b.Entity<TodoItem>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Title).IsRequired().HasMaxLength(200);
            e.Property(x => x.Priority).HasConversion<int>();
            e.HasQueryFilter(x => !x.IsDeleted);
            e.HasIndex(x => new { x.IsDeleted, x.IsCompleted, x.Priority });
        });

        var adminRoleId = Guid.NewGuid();
        var userRoleId = Guid.NewGuid();
        b.Entity<AppRole>().HasData(
            new AppRole { Id = adminRoleId, Name = "Admin", NormalizedName = "ADMIN" },
            new AppRole { Id = userRoleId, Name = "User", NormalizedName = "USER" }
        );
    }
}
