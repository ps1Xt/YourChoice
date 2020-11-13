using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YourChoice.Api.Migrations
{
    public partial class defaultrg_date : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "VideoLink",
                table: "PostParts",
                newName: "VideoPostPart_Link");

            migrationBuilder.RenameColumn(
                name: "PhotoLink",
                table: "PostParts",
                newName: "Link");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegistrationDate",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 11, 12, 21, 52, 7, 402, DateTimeKind.Local).AddTicks(607),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "VideoPostPart_Link",
                table: "PostParts",
                newName: "VideoLink");

            migrationBuilder.RenameColumn(
                name: "Link",
                table: "PostParts",
                newName: "PhotoLink");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegistrationDate",
                table: "Users",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 11, 12, 21, 52, 7, 402, DateTimeKind.Local).AddTicks(607));

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
