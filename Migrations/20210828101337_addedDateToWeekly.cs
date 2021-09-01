using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NikeFarms.v2._0.Migrations
{
    public partial class addedDateToWeekly : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "WeeklyReports",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 8, 28, 11, 13, 35, 483, DateTimeKind.Local).AddTicks(214));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 8, 28, 11, 13, 35, 489, DateTimeKind.Local).AddTicks(362));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 8, 28, 11, 13, 35, 480, DateTimeKind.Local).AddTicks(3474));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "WeeklyReports");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 8, 27, 15, 10, 53, 679, DateTimeKind.Local).AddTicks(5495));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 8, 27, 15, 10, 53, 685, DateTimeKind.Local).AddTicks(912));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 8, 27, 15, 10, 53, 677, DateTimeKind.Local).AddTicks(1698));
        }
    }
}
