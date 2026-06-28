using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniProjectAuthentication.Repo.Entity;

namespace MiniProjectAuthentication.Repo.Configurations.Seed;

public static class RefreshTokenSeed
{
    public static void RefreshTokenSeedData(this EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasData(
            new RefreshToken
            {
                Id = Guid.Parse("c01a2b3c-4d5e-6f7a-8b9c-0d1e2f3a4b5c"),
                UserId = UserSeed.AdminId,
                TokenHash = "v3_hash_abcdef1234567890_token_string_here",
                ExpiredAt = new DateTime(2026, 7, 30, 0, 0, 0, DateTimeKind.Utc),
                Revoked = false,
                RevokedAt = null,
                DeviceName = "Chrome / Windows 11",
                IpAddress = "192.168.1.5",
                CreatedAt = new DateTime(2026, 6, 28, 10, 0, 0, DateTimeKind.Utc)
            },
            new RefreshToken
            {
                Id = Guid.Parse("d02b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d"),
                UserId = UserSeed.UserId, 
                TokenHash = "v3_hash_9876543210fedcba_old_token_string",
                ExpiredAt = new DateTime(2026, 6, 15, 0, 0, 0, DateTimeKind.Utc),
                Revoked = true,
                RevokedAt = new DateTime(2026, 6, 10, 15, 30, 0, DateTimeKind.Utc),
                DeviceName = "Safari / iPhone 15 Pro",
                IpAddress = "172.16.0.42",
                CreatedAt = new DateTime(2026, 6, 1, 10, 35, 0, DateTimeKind.Utc)
            }
        );
    }
}