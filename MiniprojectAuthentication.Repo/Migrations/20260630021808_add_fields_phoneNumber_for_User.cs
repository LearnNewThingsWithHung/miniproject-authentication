using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniprojectAuthentication.Repo.Migrations
{
    /// <inheritdoc />
    public partial class add_fields_phoneNumber_for_User : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3f8e2d1c-bc5a-4912-8fcd-987654fedcba"),
                columns: new[] { "PhoneNumber", "PhoneNumberConfirmed" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7a2b9c1d-ef4a-4b23-a123-456789abcdef"),
                columns: new[] { "PhoneNumber", "PhoneNumberConfirmed" },
                values: new object[] { null, false });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "Users");
        }
    }
}
