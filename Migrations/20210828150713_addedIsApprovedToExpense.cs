using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NikeFarms.v2._0.Migrations
{
    public partial class addedIsApprovedToExpense : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Expenses",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 8, 28, 16, 7, 11, 881, DateTimeKind.Local).AddTicks(8221));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 8, 28, 16, 7, 11, 897, DateTimeKind.Local).AddTicks(999));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 8, 28, 16, 7, 11, 878, DateTimeKind.Local).AddTicks(3472));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Expenses");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 8, 28, 12, 34, 24, 331, DateTimeKind.Local).AddTicks(2225));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 8, 28, 12, 34, 24, 336, DateTimeKind.Local).AddTicks(8642));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 8, 28, 12, 34, 24, 328, DateTimeKind.Local).AddTicks(8290));
        }
    }
}
