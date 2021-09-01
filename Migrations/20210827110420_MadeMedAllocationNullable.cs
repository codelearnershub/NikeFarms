using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NikeFarms.v2._0.Migrations
{
    public partial class MadeMedAllocationNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyActivities_StoreAllocations_StoreAllocationMedId",
                table: "DailyActivities");

            migrationBuilder.DropColumn(
                name: "FeedConsumedPerKg",
                table: "DailyActivities");

            migrationBuilder.AlterColumn<int>(
                name: "StoreAllocationMedId",
                table: "DailyActivities",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 8, 27, 12, 4, 19, 132, DateTimeKind.Local).AddTicks(6861));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 8, 27, 12, 4, 19, 140, DateTimeKind.Local).AddTicks(1532));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 8, 27, 12, 4, 19, 128, DateTimeKind.Local).AddTicks(4765));

            migrationBuilder.AddForeignKey(
                name: "FK_DailyActivities_StoreAllocations_StoreAllocationMedId",
                table: "DailyActivities",
                column: "StoreAllocationMedId",
                principalTable: "StoreAllocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyActivities_StoreAllocations_StoreAllocationMedId",
                table: "DailyActivities");

            migrationBuilder.AlterColumn<int>(
                name: "StoreAllocationMedId",
                table: "DailyActivities",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FeedConsumedPerKg",
                table: "DailyActivities",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 8, 26, 17, 41, 51, 286, DateTimeKind.Local).AddTicks(2021));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 8, 26, 17, 41, 51, 301, DateTimeKind.Local).AddTicks(4556));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 8, 26, 17, 41, 51, 279, DateTimeKind.Local).AddTicks(5087));

            migrationBuilder.AddForeignKey(
                name: "FK_DailyActivities_StoreAllocations_StoreAllocationMedId",
                table: "DailyActivities",
                column: "StoreAllocationMedId",
                principalTable: "StoreAllocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
