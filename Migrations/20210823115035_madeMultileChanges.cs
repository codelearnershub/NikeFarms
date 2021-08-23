using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NikeFarms.v2._0.Migrations
{
    public partial class madeMultileChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "StoreItems",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "ItemRemaining",
                table: "StoreItems",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "StoreAllocations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "ItemRemaining",
                table: "StoreAllocations",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "ItemType",
                table: "StoreAllocations",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSold",
                table: "Sales",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Flocks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Customers",
                nullable: true);

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
                column: "CreatedAt",
                value: new DateTime(2021, 8, 23, 12, 50, 34, 421, DateTimeKind.Local).AddTicks(9868));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "StoreItems");

            migrationBuilder.DropColumn(
                name: "ItemRemaining",
                table: "StoreItems");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "StoreAllocations");

            migrationBuilder.DropColumn(
                name: "ItemRemaining",
                table: "StoreAllocations");

            migrationBuilder.DropColumn(
                name: "ItemType",
                table: "StoreAllocations");

            migrationBuilder.DropColumn(
                name: "IsSold",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Flocks");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Customers");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 8, 19, 21, 10, 9, 329, DateTimeKind.Local).AddTicks(7759));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 8, 19, 21, 10, 9, 336, DateTimeKind.Local).AddTicks(3002));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 8, 19, 21, 10, 9, 224, DateTimeKind.Local).AddTicks(2522));
        }
    }
}
