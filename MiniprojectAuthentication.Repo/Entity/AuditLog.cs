using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using MiniProjectAuthentication.Repo.Abstraction;

namespace MiniProjectAuthentication.Repo.Entity;

public class AuditLog: BaseEntity<Guid>, IAuditableEntity
{
    
    public Guid Id { get; set; }
    public string Action { get; set; } = null!;
    public string? Description { get; set; }
    [Column(TypeName = "jsonb")]
    public string OldValue { get; set; }
    [Column(TypeName = "jsonb")]
    public string? NewValue { get; set; }
    
    public string Entity { get; set; } = null!;
    public Guid EntityId { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}