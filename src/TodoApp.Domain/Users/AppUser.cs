
using Microsoft.AspNetCore.Identity;

namespace TodoApp.Domain.Users;

public class AppUser : IdentityUser<Guid>
{
    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;
}
