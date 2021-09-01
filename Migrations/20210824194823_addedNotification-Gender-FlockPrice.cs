using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace NikeFarms.v2._0.Migrations
{
    public partial class addedNotificationGenderFlockPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Users",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "FlockTypes",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<decimal>(
                name: "AmountPurchased",
                table: "Flocks",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    Content = table.Column<string>(nullable: false),
                    RecieverId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_RecieverId",
                        column: x => x.RecieverId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 8, 24, 20, 48, 22, 454, DateTimeKind.Local).AddTicks(8532));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 8, 24, 20, 48, 22, 464, DateTimeKind.Local).AddTicks(1591));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Gender", "HashSalt", "PasswordHash" },
                values: new object[] { new DateTime(2021, 8, 24, 20, 48, 22, 450, DateTimeKind.Local).AddTicks(9609), "Female", "guDA4rCIrHxbmB2fm1lRCw==", "ZIoFJMM2K8J24l9L3yXaVYnFKKca+dIlRaPO9OJVB0g=" });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_RecieverId",
                table: "Notifications",
                column: "RecieverId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AmountPurchased",
                table: "Flocks");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "FlockTypes",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 8, 23, 12, 50, 34, 424, DateTimeKind.Local).AddTicks(5033));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 8, 23, 12, 50, 34, 429, DateTimeKind.Local).AddTicks(9632));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "HashSalt", "PasswordHash" },
                values: new object[] { new DateTime(2021, 8, 23, 12, 50, 34, 421, DateTimeKind.Local).AddTicks(9868), "J5cdgq6p63lKwmVg3b7ltQ==", "EGSROdXOgd5YsDofKkZatVGf2Cnc/7O/RxhqQSmOF30=" });
        }
    }
}
