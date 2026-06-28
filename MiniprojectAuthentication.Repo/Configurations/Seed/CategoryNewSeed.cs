using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniProjectAuthentication.Repo.Entity;

namespace MiniProjectAuthentication.Repo.Configurations.Seed;

public static class CategoryNewSeed
{
    public static void CategoryNewSeedData(this EntityTypeBuilder<CategoryNew> builder)
    {
        builder.HasData(
            new CategoryNew
            {
                Id = Guid.Parse("a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d"),
                CategoryId = CategorySeed.PhoneCategoryId, 
                NewsId = NewSeed.NewsId1 
            },
            new CategoryNew
            {
                Id = Guid.Parse("f6e5d4c3-b2a1-0f9e-8d7c-6b5a4b3c2d1e"),
                CategoryId = CategorySeed.LaptopCategoryId, 
                NewsId = NewSeed.NewsId2 
            }
        );
    }
}