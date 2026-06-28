using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniProjectAuthentication.Repo.Entity;

namespace MiniProjectAuthentication.Repo.Configurations.Seed;

public static class NewSeed
{
    public static readonly Guid NewsId1 = Guid.Parse("fa112233-4455-6677-8899-aabbccddeeff");
    public static readonly Guid NewsId2 = Guid.Parse("fb223344-5566-7788-99aa-bbccddeeff11");

    public static void NewSeedData(this EntityTypeBuilder<New> builder)
    {
        builder.HasData(
            new New
            {
                Id = NewsId1,
                UserId = UserSeed.AdminId, 
                Title = "Đánh giá chi tiết iPhone 17 Pro Max sau 1 tuần sử dụng",
                Description = "Bài viết phân tích sâu về hiệu năng, thời lượng pin và camera của thế hệ iPhone mới nhất năm 2026.",
                ViewCount = 150,
                PictureUrl = "https://cdn.example.com/images/iphone17-review.jpg",
                Status = "Published",
                CreatedAt = new DateTime(2026, 6, 25, 8, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 6, 25, 8, 0, 0, DateTimeKind.Utc)
            },
            new New
            {
                Id = NewsId2,
                UserId = UserSeed.AdminId,
                Title = "Top 5 Laptop Gaming đáng mua nhất nửa đầu năm 2026",
                Description = "Tổng hợp danh sách những chiếc laptop gaming mạnh mẽ, tối ưu phân khúc giá từ tầm trung đến cao cấp.",
                ViewCount = 89,
                PictureUrl = "https://cdn.example.com/images/top-laptops-2026.jpg",
                Status = "Published",
                CreatedAt = new DateTime(2026, 6, 27, 14, 20, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 6, 27, 14, 20, 0, DateTimeKind.Utc)
            }
        );
    }
}