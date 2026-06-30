using MiniProjectAuthentication.Repo.Abstraction;

namespace MiniProjectAuthentication.Repo.Entity;

public class RefreshToken: BaseEntity<Guid>, IAuditableEntity
{
    
    public Guid Id { get; set; }
    public string TokenHash { get; set; } = null!;
    public DateTime ExpiredAt { get; set; }
    public bool Revoked { get; set; } = false;
    public DateTime? RevokedAt { get; set; }
    
    
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}