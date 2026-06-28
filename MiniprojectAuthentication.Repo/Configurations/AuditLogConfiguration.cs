using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniProjectAuthentication.Repo.Configurations.Seed;
using MiniProjectAuthentication.Repo.Entity;

namespace MiniProjectAuthentication.Repo.Configurations;

public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
            
        builder.Property(e => e.Action).IsRequired().HasComment("Hành động (Ví dụ: CREATE, UPDATE, DELETE)");
        builder.Property(e => e.Description).HasComment("Mô tả chi tiết hành động");
        builder.Property(e => e.OldValue)
            .HasColumnType("jsonb")
            .HasComment("Giá trị cũ trước khi sửa (thường lưu JSON)");
        builder.Property(e => e.NewValue)
            .HasColumnType("jsonb")
            .HasComment("Giá trị mới sau khi sửa (thường lưu JSON)");
        builder.Property(e => e.Entity).IsRequired().HasComment("Tên bảng/thực thể bị tác động (Ví dụ: User, Product)");
        builder.Property(e => e.CreatedAt).HasDefaultValueSql("now()");


        builder.HasOne(d => d.User)
            .WithMany(p => p.AuditLogs)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.AuditLogSeedData();
    }
}