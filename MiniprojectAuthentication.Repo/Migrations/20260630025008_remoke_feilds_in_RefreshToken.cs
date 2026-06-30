using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniprojectAuthentication.Repo.Migrations
{
    /// <inheritdoc />
    public partial class remoke_feilds_in_RefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceName",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "RefreshTokens");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeviceName",
                table: "RefreshTokens",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "RefreshTokens",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "RefreshTokens",
                keyColumn: "Id",
                keyValue: new Guid("c01a2b3c-4d5e-6f7a-8b9c-0d1e2f3a4b5c"),
                columns: new[] { "DeviceName", "IpAddress" },
                values: new object[] { "Chrome / Windows 11", "192.168.1.5" });

            migrationBuilder.UpdateData(
                table: "RefreshTokens",
                keyColumn: "Id",
                keyValue: new Guid("d02b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d"),
                columns: new[] { "DeviceName", "IpAddress" },
                values: new object[] { "Safari / iPhone 15 Pro", "172.16.0.42" });
        }
    }
}
