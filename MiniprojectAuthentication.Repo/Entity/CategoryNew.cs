using MiniProjectAuthentication.Repo.Abstraction;

namespace MiniProjectAuthentication.Repo.Entity;

public class CategoryNew: BaseEntity<Guid>, IAuditableEntity
{
    
    public Guid Id { get; set; }

    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public Guid NewsId { get; set; }
    public New New { get; set; } = null!;
    
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    
}