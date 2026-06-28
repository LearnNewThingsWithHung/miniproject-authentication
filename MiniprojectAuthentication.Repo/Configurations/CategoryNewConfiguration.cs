using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniProjectAuthentication.Repo.Configurations.Seed;
using MiniProjectAuthentication.Repo.Entity;

namespace MiniProjectAuthentication.Repo.Configurations;

public class CategoryNewConfiguration : IEntityTypeConfiguration<CategoryNew>
{
    public void Configure(EntityTypeBuilder<CategoryNew> builder)
    {
        builder.HasOne(d => d.New)
            .WithMany(p => p.CategoryNews)
            .HasForeignKey(d => d.NewsId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(d => d.Category)
            .WithMany(p => p.CategoryNews)
            .HasForeignKey(d => d.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.CategoryNewSeedData();
    }
}