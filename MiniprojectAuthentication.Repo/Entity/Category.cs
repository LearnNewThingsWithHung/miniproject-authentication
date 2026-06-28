using MiniProjectAuthentication.Repo.Abstraction;

namespace MiniProjectAuthentication.Repo.Entity;

public class Category: BaseEntity<Guid>, IAuditableEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;

    public Guid UserId { get; set; }
    public  User User { get; set; } = null!;
    public Guid? ParentId { get; set; }
    public  Category? Parent { get; set; }
    public  ICollection<Category> Children { get; set; } = new List<Category>();
    public  ICollection<CategoryNew> CategoryNews { get; set; } = new List<CategoryNew>();
    
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

}