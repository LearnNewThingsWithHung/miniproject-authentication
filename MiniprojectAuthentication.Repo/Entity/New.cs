using MiniProjectAuthentication.Repo.Abstraction;

namespace MiniProjectAuthentication.Repo.Entity;

public class New: BaseEntity<Guid>, IAuditableEntity
{
    
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public int ViewCount { get; set; } = 0;
    public string? PictureUrl { get; set; }
    public string? Status { get; set; }

    public User User { get; set; } = null!;
    public Guid UserId { get; set; }
    public ICollection<CategoryNew> CategoryNews{ get; set; } = new List<CategoryNew>();
    
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}