using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniProjectAuthentication.Repo.Configurations.Seed;
using MiniProjectAuthentication.Repo.Entity;

namespace MiniProjectAuthentication.Repo.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
            
        builder.Property(e => e.TokenHash).IsRequired().HasComment("Hash của Refresh Token");
        builder.Property(e => e.Revoked).HasDefaultValue(false).HasComment("Đã bị thu hồi/vô hiệu hóa chưa");
        builder.Property(e => e.CreatedAt).HasDefaultValueSql("now()");

        builder.HasOne(d => d.User)
            .WithMany(p => p.RefreshTokens)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.RefreshTokenSeedData();
    }
}