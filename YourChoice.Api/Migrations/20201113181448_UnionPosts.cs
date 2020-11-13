using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YourChoice.Api.Migrations
{
    public partial class UnionPosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostParts_Posts_PhotoPostId",
                table: "PostParts");

            migrationBuilder.DropForeignKey(
                name: "FK_PostParts_Posts_VideoPostId",
                table: "PostParts");

            migrationBuilder.DropIndex(
                name: "IX_PostParts_PhotoPostId",
                table: "PostParts");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "PostParts");

            migrationBuilder.DropColumn(
                name: "PhotoPostId",
                table: "PostParts");

            migrationBuilder.DropColumn(
                name: "VideoPostPart_Link",
                table: "PostParts");

            migrationBuilder.RenameColumn(
                name: "VideoPostId",
                table: "PostParts",
                newName: "PostId");

            migrationBuilder.RenameIndex(
                name: "IX_PostParts_VideoPostId",
                table: "PostParts",
                newName: "IX_PostParts_PostId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegistrationDate",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 11, 13, 20, 14, 47, 976, DateTimeKind.Local).AddTicks(6043),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 11, 12, 21, 52, 7, 402, DateTimeKind.Local).AddTicks(607));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Posts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 11, 13, 20, 14, 47, 952, DateTimeKind.Local).AddTicks(8938),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 11, 13, 20, 14, 47, 967, DateTimeKind.Local).AddTicks(426),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddForeignKey(
                name: "FK_PostParts_Posts_PostId",
                table: "PostParts",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostParts_Posts_PostId",
                table: "PostParts");

            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "PostParts",
                newName: "VideoPostId");

            migrationBuilder.RenameIndex(
                name: "IX_PostParts_PostId",
                table: "PostParts",
                newName: "IX_PostParts_VideoPostId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegistrationDate",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 11, 12, 21, 52, 7, 402, DateTimeKind.Local).AddTicks(607),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 11, 13, 20, 14, 47, 976, DateTimeKind.Local).AddTicks(6043));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Posts",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 11, 13, 20, 14, 47, 952, DateTimeKind.Local).AddTicks(8938));

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "PostParts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PhotoPostId",
                table: "PostParts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VideoPostPart_Link",
                table: "PostParts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 11, 13, 20, 14, 47, 967, DateTimeKind.Local).AddTicks(426));

            migrationBuilder.CreateIndex(
                name: "IX_PostParts_PhotoPostId",
                table: "PostParts",
                column: "PhotoPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostParts_Posts_PhotoPostId",
                table: "PostParts",
                column: "PhotoPostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostParts_Posts_VideoPostId",
                table: "PostParts",
                column: "VideoPostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
