using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NikeFarms.v2._0.Migrations
{
    public partial class addedNotificationGenderFlockPrice2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApproveId",
                table: "Notifications",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 8, 24, 21, 32, 10, 466, DateTimeKind.Local).AddTicks(8504));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 8, 24, 21, 32, 10, 480, DateTimeKind.Local).AddTicks(5897));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 8, 24, 21, 32, 10, 464, DateTimeKind.Local).AddTicks(2492));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApproveId",
                table: "Notifications");

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
                column: "CreatedAt",
                value: new DateTime(2021, 8, 24, 20, 48, 22, 450, DateTimeKind.Local).AddTicks(9609));
        }
    }
}
