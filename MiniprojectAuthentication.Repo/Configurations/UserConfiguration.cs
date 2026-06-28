using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniProjectAuthentication.Repo.Configurations.Seed;
using MiniProjectAuthentication.Repo.Entity;
using MiniProjectAuthentication.Repo.Enum;

namespace MiniProjectAuthentication.Repo.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
            
        builder.HasIndex(e => e.Email).IsUnique();
        builder.Property(e => e.Email).IsRequired().HasMaxLength(256);
        
        builder.Property(x => x.Role)
            .HasConversion<string>()
            .HasMaxLength(50);
        
        builder.Property(e => e.PasswordHash).IsRequired().HasComment("Mật khẩu đã được hash");
        builder.Property(e => e.EmailVerified).HasDefaultValue(false);
        builder.Property(e => e.IsLocked).HasDefaultValue(false).HasComment("Trạng thái khóa tài khoản");
            
        builder.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
        builder.UserSeedData();
    }
    
}