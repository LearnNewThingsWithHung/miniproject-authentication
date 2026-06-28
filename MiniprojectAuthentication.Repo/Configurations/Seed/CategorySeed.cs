using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniProjectAuthentication.Repo.Entity;

namespace MiniProjectAuthentication.Repo.Configurations.Seed;

public static class CategorySeed
    {
        public static readonly Guid TechCategoryId   = Guid.Parse("11a2b3c4-5d6e-7f8a-9b0c-1d2e3f4a5b6c");
        public static readonly Guid PhoneCategoryId  = Guid.Parse("22b3c4d5-6e7f-8a9b-0c1d-2e3f4a5b6c7d");
        public static readonly Guid LaptopCategoryId = Guid.Parse("33c4d5e6-7f8a-9b0c-1d2e-3f4a5b6c7d8e");
        public static readonly Guid AiCategoryId     = Guid.Parse("44d5e6f7-8a9b-0c1d-2e3f-4a5b6c7d8e9f");
    
        public static readonly Guid LifeCategoryId   = Guid.Parse("55e6f7a8-9b0c-1d2e-3f4a-5b6c7d8e9f0a");
        public static readonly Guid MovieCategoryId  = Guid.Parse("66f7a8b9-0c1d-2e3f-4a5b-6c7d8e9f0a1b");
        public static readonly Guid TravelCategoryId = Guid.Parse("77a8b9c0-1d2e-3f4a-5b6c-7d8e9f0a1b2c");

        public static void CategorySeedData(this EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                new Category
                {
                    Id = TechCategoryId,
                    UserId = UserSeed.AdminId, 
                    ParentId = null,           
                    Title = "Công nghệ"
                },
                new Category
                {
                    Id = PhoneCategoryId,
                    UserId = UserSeed.AdminId,
                    ParentId = TechCategoryId, 
                    Title = "Điện thoại"
                },
                new Category
                {
                    Id = LaptopCategoryId,
                    UserId = UserSeed.AdminId,
                    ParentId = TechCategoryId, 
                    Title = "Laptop"
                },
                new Category
                {
                    Id = AiCategoryId,
                    UserId = UserSeed.AdminId,
                    ParentId = TechCategoryId,
                    Title = "Trí tuệ nhân tạo (AI)"
                },
                new Category
                {
                    Id = LifeCategoryId,
                    UserId = UserSeed.AdminId,
                    ParentId = null,           
                    Title = "Giải trí & Đời sống"
                },
                new Category
                {
                    Id = MovieCategoryId,
                    UserId = UserSeed.AdminId,
                    ParentId = LifeCategoryId, 
                    Title = "Điện ảnh / Phim truyền hình"
                },
                new Category
                {
                    Id = TravelCategoryId,
                    UserId = UserSeed.AdminId,
                    ParentId = LifeCategoryId, 
                    Title = "Du lịch & Khám phá"
                }
            );
        }
    }