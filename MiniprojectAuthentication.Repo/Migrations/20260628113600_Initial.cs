using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiniprojectAuthentication.Repo.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Role = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false, comment: "Mật khẩu đã được hash"),
                    EmailVerified = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IsLocked = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false, comment: "Trạng thái khóa tài khoản"),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Action = table.Column<string>(type: "text", nullable: false, comment: "Hành động (Ví dụ: CREATE, UPDATE, DELETE)"),
                    Description = table.Column<string>(type: "text", nullable: true, comment: "Mô tả chi tiết hành động"),
                    OldValue = table.Column<string>(type: "jsonb", nullable: false, comment: "Giá trị cũ trước khi sửa (thường lưu JSON)"),
                    NewValue = table.Column<string>(type: "jsonb", nullable: true, comment: "Giá trị mới sau khi sửa (thường lưu JSON)"),
                    Entity = table.Column<string>(type: "text", nullable: false, comment: "Tên bảng/thực thể bị tác động (Ví dụ: User, Product)"),
                    EntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditLogs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Categories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ViewCount = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    PictureUrl = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                    table.ForeignKey(
                        name: "FK_News_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TokenHash = table.Column<string>(type: "text", nullable: false, comment: "Hash của Refresh Token"),
                    ExpiredAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Revoked = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false, comment: "Đã bị thu hồi/vô hiệu hóa chưa"),
                    RevokedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeviceName = table.Column<string>(type: "text", nullable: true),
                    IpAddress = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryNews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    NewsId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryNews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryNews_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryNews_News_NewsId",
                        column: x => x.NewsId,
                        principalTable: "News",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "IsDeleted", "PasswordHash", "Role", "UpdatedAt" },
                values: new object[] { new Guid("3f8e2d1c-bc5a-4912-8fcd-987654fedcba"), new DateTimeOffset(new DateTime(2026, 6, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "nguyenvana@gmail.com", false, "Hung2006@", "Customer", new DateTimeOffset(new DateTime(2026, 6, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "EmailVerified", "IsDeleted", "PasswordHash", "Role", "UpdatedAt" },
                values: new object[] { new Guid("7a2b9c1d-ef4a-4b23-a123-456789abcdef"), new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "admin@identityservice.com", true, false, "Hung2006@", "Admin", new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.InsertData(
                table: "AuditLogs",
                columns: new[] { "Id", "Action", "CreatedAt", "Description", "Entity", "EntityId", "IsDeleted", "NewValue", "OldValue", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("e01b2c3d-4e5f-6a7b-8c9d-0e1f2a3b4c5d"), "CREATE", new DateTimeOffset(new DateTime(2026, 6, 20, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin tạo danh mục tin tức mới: Công nghệ", "Category", new Guid("11a2b3c4-5d6e-7f8a-9b0c-1d2e3f4a5b6c"), false, "{\"Id\":\"11a2b3c4-5d6e-7f8a-9b0c-1d2e3f4a5b6c\",\"Title\":\"Công nghệ\",\"ParentId\":null}", "{}", null, new Guid("7a2b9c1d-ef4a-4b23-a123-456789abcdef") },
                    { new Guid("f02c3d4e-5f6a-7b8c-9d0e-1f2a3b4c5d6e"), "UPDATE", new DateTimeOffset(new DateTime(2026, 6, 28, 16, 45, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Thay đổi trạng thái xác thực email của người dùng Nguyễn Văn A", "User", new Guid("3f8e2d1c-bc5a-4912-8fcd-987654fedcba"), false, "{\"EmailVerified\":true}", "{\"EmailVerified\":false}", null, new Guid("7a2b9c1d-ef4a-4b23-a123-456789abcdef") }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "IsDeleted", "ParentId", "Title", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("11a2b3c4-5d6e-7f8a-9b0c-1d2e3f4a5b6c"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, "Công nghệ", null, new Guid("7a2b9c1d-ef4a-4b23-a123-456789abcdef") },
                    { new Guid("55e6f7a8-9b0c-1d2e-3f4a-5b6c7d8e9f0a"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, "Giải trí & Đời sống", null, new Guid("7a2b9c1d-ef4a-4b23-a123-456789abcdef") }
                });

            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "Id", "CreatedAt", "Description", "IsDeleted", "PictureUrl", "Status", "Title", "UpdatedAt", "UserId", "ViewCount" },
                values: new object[,]
                {
                    { new Guid("fa112233-4455-6677-8899-aabbccddeeff"), new DateTimeOffset(new DateTime(2026, 6, 25, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Bài viết phân tích sâu về hiệu năng, thời lượng pin và camera của thế hệ iPhone mới nhất năm 2026.", false, "https://cdn.example.com/images/iphone17-review.jpg", "Published", "Đánh giá chi tiết iPhone 17 Pro Max sau 1 tuần sử dụng", new DateTimeOffset(new DateTime(2026, 6, 25, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("7a2b9c1d-ef4a-4b23-a123-456789abcdef"), 150 },
                    { new Guid("fb223344-5566-7788-99aa-bbccddeeff11"), new DateTimeOffset(new DateTime(2026, 6, 27, 14, 20, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Tổng hợp danh sách những chiếc laptop gaming mạnh mẽ, tối ưu phân khúc giá từ tầm trung đến cao cấp.", false, "https://cdn.example.com/images/top-laptops-2026.jpg", "Published", "Top 5 Laptop Gaming đáng mua nhất nửa đầu năm 2026", new DateTimeOffset(new DateTime(2026, 6, 27, 14, 20, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("7a2b9c1d-ef4a-4b23-a123-456789abcdef"), 89 }
                });

            migrationBuilder.InsertData(
                table: "RefreshTokens",
                columns: new[] { "Id", "CreatedAt", "DeviceName", "ExpiredAt", "IpAddress", "IsDeleted", "RevokedAt", "TokenHash", "UpdatedAt", "UserId" },
                values: new object[] { new Guid("c01a2b3c-4d5e-6f7a-8b9c-0d1e2f3a4b5c"), new DateTimeOffset(new DateTime(2026, 6, 28, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Chrome / Windows 11", new DateTime(2026, 7, 30, 0, 0, 0, 0, DateTimeKind.Utc), "192.168.1.5", false, null, "v3_hash_abcdef1234567890_token_string_here", null, new Guid("7a2b9c1d-ef4a-4b23-a123-456789abcdef") });

            migrationBuilder.InsertData(
                table: "RefreshTokens",
                columns: new[] { "Id", "CreatedAt", "DeviceName", "ExpiredAt", "IpAddress", "IsDeleted", "Revoked", "RevokedAt", "TokenHash", "UpdatedAt", "UserId" },
                values: new object[] { new Guid("d02b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d"), new DateTimeOffset(new DateTime(2026, 6, 1, 10, 35, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Safari / iPhone 15 Pro", new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), "172.16.0.42", false, true, new DateTime(2026, 6, 10, 15, 30, 0, 0, DateTimeKind.Utc), "v3_hash_9876543210fedcba_old_token_string", null, new Guid("3f8e2d1c-bc5a-4912-8fcd-987654fedcba") });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "IsDeleted", "ParentId", "Title", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("22b3c4d5-6e7f-8a9b-0c1d-2e3f4a5b6c7d"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("11a2b3c4-5d6e-7f8a-9b0c-1d2e3f4a5b6c"), "Điện thoại", null, new Guid("7a2b9c1d-ef4a-4b23-a123-456789abcdef") },
                    { new Guid("33c4d5e6-7f8a-9b0c-1d2e-3f4a5b6c7d8e"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("11a2b3c4-5d6e-7f8a-9b0c-1d2e3f4a5b6c"), "Laptop", null, new Guid("7a2b9c1d-ef4a-4b23-a123-456789abcdef") },
                    { new Guid("44d5e6f7-8a9b-0c1d-2e3f-4a5b6c7d8e9f"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("11a2b3c4-5d6e-7f8a-9b0c-1d2e3f4a5b6c"), "Trí tuệ nhân tạo (AI)", null, new Guid("7a2b9c1d-ef4a-4b23-a123-456789abcdef") },
                    { new Guid("66f7a8b9-0c1d-2e3f-4a5b-6c7d8e9f0a1b"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("55e6f7a8-9b0c-1d2e-3f4a-5b6c7d8e9f0a"), "Điện ảnh / Phim truyền hình", null, new Guid("7a2b9c1d-ef4a-4b23-a123-456789abcdef") },
                    { new Guid("77a8b9c0-1d2e-3f4a-5b6c-7d8e9f0a1b2c"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("55e6f7a8-9b0c-1d2e-3f4a-5b6c7d8e9f0a"), "Du lịch & Khám phá", null, new Guid("7a2b9c1d-ef4a-4b23-a123-456789abcdef") }
                });

            migrationBuilder.InsertData(
                table: "CategoryNews",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "IsDeleted", "NewsId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d"), new Guid("22b3c4d5-6e7f-8a9b-0c1d-2e3f4a5b6c7d"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("fa112233-4455-6677-8899-aabbccddeeff"), null },
                    { new Guid("f6e5d4c3-b2a1-0f9e-8d7c-6b5a4b3c2d1e"), new Guid("33c4d5e6-7f8a-9b0c-1d2e-3f4a5b6c7d8e"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("fb223344-5566-7788-99aa-bbccddeeff11"), null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_UserId",
                table: "AuditLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentId",
                table: "Categories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_UserId",
                table: "Categories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryNews_CategoryId",
                table: "CategoryNews",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryNews_NewsId",
                table: "CategoryNews",
                column: "NewsId");

            migrationBuilder.CreateIndex(
                name: "IX_News_UserId",
                table: "News",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "CategoryNews");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
