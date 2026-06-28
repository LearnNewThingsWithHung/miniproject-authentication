using MiniProjectAuthentication.Repo.Abstraction;
using MiniProjectAuthentication.Repo.Enum;

namespace MiniProjectAuthentication.Repo.Entity;

public class User: BaseEntity<Guid>, IAuditableEntity
{
    
    public Guid Id { get; set; }
    public Role Role { get; set; } = Role.Customer;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public bool EmailVerified { get; set; } = false;
    public bool IsLocked { get; set; } = false;
    
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();
    public ICollection<Category> Categories { get; set; } = new List<Category>();
    public ICollection<New> News { get; set; } = new List<New>();
    
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    
}
