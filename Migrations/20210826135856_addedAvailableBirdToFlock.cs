using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NikeFarms.v2._0.Migrations
{
    public partial class addedAvailableBirdToFlock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AvailableBirds",
                table: "Flocks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 8, 26, 14, 58, 55, 634, DateTimeKind.Local).AddTicks(5623));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 8, 26, 14, 58, 55, 657, DateTimeKind.Local).AddTicks(2014));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 8, 26, 14, 58, 55, 631, DateTimeKind.Local).AddTicks(8307));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableBirds",
                table: "Flocks");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 8, 25, 22, 30, 34, 488, DateTimeKind.Local).AddTicks(4396));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 8, 25, 22, 30, 34, 494, DateTimeKind.Local).AddTicks(9344));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 8, 25, 22, 30, 34, 485, DateTimeKind.Local).AddTicks(6670));
        }
    }
}
