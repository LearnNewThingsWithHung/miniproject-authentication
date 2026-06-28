using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniProjectAuthentication.Repo.Entity;

namespace MiniProjectAuthentication.Repo.Configurations.Seed;

public static class AuditLogSeed
{
    public static void AuditLogSeedData(this EntityTypeBuilder<AuditLog> builder)
    {
        builder.HasData(
            new AuditLog
            {
                Id = Guid.Parse("e01b2c3d-4e5f-6a7b-8c9d-0e1f2a3b4c5d"),
                UserId = UserSeed.AdminId, 
                Action = "CREATE",
                Description = "Admin tạo danh mục tin tức mới: Công nghệ",
                OldValue = "{}",
                NewValue = "{\"Id\":\"11a2b3c4-5d6e-7f8a-9b0c-1d2e3f4a5b6c\",\"Title\":\"Công nghệ\",\"ParentId\":null}",
                Entity = "Category",
                EntityId = CategorySeed.TechCategoryId, 
                CreatedAt = new DateTime(2026, 6, 20, 9, 0, 0, DateTimeKind.Utc)
            },
            // Log 2: Hệ thống hoặc Admin khóa tài khoản vi phạm (Ví dụ)
            new AuditLog
            {
                Id = Guid.Parse("f02c3d4e-5f6a-7b8c-9d0e-1f2a3b4c5d6e"),
                UserId = UserSeed.AdminId,
                Action = "UPDATE",
                Description = "Thay đổi trạng thái xác thực email của người dùng Nguyễn Văn A",
                OldValue = "{\"EmailVerified\":false}",
                NewValue = "{\"EmailVerified\":true}",
                Entity = "User",
                EntityId = UserSeed.UserId, // Tác động lên User Nguyễn Văn A
                CreatedAt = new DateTime(2026, 6, 28, 16, 45, 0, DateTimeKind.Utc)
            }
        );
    }
}