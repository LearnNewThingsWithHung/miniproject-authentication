using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniProjectAuthentication.Repo.Entity;
using MiniProjectAuthentication.Repo.Enum;

namespace MiniProjectAuthentication.Repo.Configurations.Seed;

public static class UserSeed
{ 
    public static readonly Guid AdminId = Guid.Parse("7a2b9c1d-ef4a-4b23-a123-456789abcdef");
    public static readonly Guid UserId = Guid.Parse("3f8e2d1c-bc5a-4912-8fcd-987654fedcba");

    public static void UserSeedData(this EntityTypeBuilder<User> builder)
    {
        builder.HasData(
            new User
            {
                Id = AdminId,
                Role = Role.Admin,
                Email = "admin@identityservice.com",
                PasswordHash = "Hung2006@", 
                EmailVerified = true,
                IsLocked = false,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                Id = UserId,
                Role = Role.Customer,
                Email = "nguyenvana@gmail.com",
                PasswordHash = "Hung2006@",
                EmailVerified = false,
                IsLocked = false,
                CreatedAt = new DateTime(2026, 6, 1, 10, 30, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 6, 1, 10, 30, 0, DateTimeKind.Utc)
            }
        );
    }
}