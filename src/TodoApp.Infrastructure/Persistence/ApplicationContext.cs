using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoApp.Domain.Todos;
using TodoApp.Domain.Users;
using TodoApp.Infrastructure.Identity;

namespace TodoApp.Infrastructure.Persistence;

public class ApplicationContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    // Use fixed GUIDs for seed data to avoid model drift
    private static readonly Guid AdminRoleId = new Guid("98e30fe3-5f7c-4c49-8faf-34d690097bda");
    private static readonly Guid UserRoleId  = new Guid("3d319abd-bc7e-46d1-bcd6-5a0c61e582b0");
    private const string AdminRoleConcurrencyStamp = "d0ee5559-3f53-4cd0-af77-8ccb23f2e8fd";
    private const string UserRoleConcurrencyStamp = "9c2a2940-e7e9-4db2-a851-89331747d2d4";

    public DbSet<TodoItem> Todos => Set<TodoItem>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

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

        b.Entity<RefreshToken>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Token).IsRequired();
            e.HasIndex(x => x.Token).IsUnique();
            e.HasIndex(x => new { x.UserId, x.Revoked, x.ExpiresUtc });
        });

        b.Entity<AppRole>().HasData(
            new AppRole
            {
                Id = AdminRoleId,
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = AdminRoleConcurrencyStamp
            },
            new AppRole
            {
                Id = UserRoleId,
                Name = "User",
                NormalizedName = "USER",
                ConcurrencyStamp = UserRoleConcurrencyStamp
            }
        );
    }
}
