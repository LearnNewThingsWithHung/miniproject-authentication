using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniProjectAuthentication.Repo.Configurations.Seed;
using MiniProjectAuthentication.Repo.Entity;

namespace MiniProjectAuthentication.Repo.Configurations;

public class NewsConfiguration : IEntityTypeConfiguration<New>
{
    public void Configure(EntityTypeBuilder<New> builder)
    {
            
        builder.Property(e => e.Title).IsRequired().HasMaxLength(500);
        builder.Property(e => e.ViewCount).HasDefaultValue(0);
        builder.Property(e => e.CreatedAt).HasDefaultValueSql("now()");


        builder.HasOne(d => d.User)
            .WithMany(p => p.News)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.NewSeedData();
    }
}