using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NikeFarms.v2._0.Migrations
{
    public partial class addTotalPriceTostoreItem2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "CreatedBy" },
                values: new object[] { new DateTime(2021, 8, 19, 21, 10, 9, 329, DateTimeKind.Local).AddTicks(7759), "mazeedahhamzat@gmail.com" });

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "CreatedBy" },
                values: new object[] { new DateTime(2021, 8, 19, 21, 10, 9, 336, DateTimeKind.Local).AddTicks(3002), "mazeedahhamzat@gmail.com" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "CreatedBy" },
                values: new object[] { new DateTime(2021, 8, 19, 21, 10, 9, 224, DateTimeKind.Local).AddTicks(2522), "mazeedahhamzat@gmail.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "CreatedBy" },
                values: new object[] { new DateTime(2021, 8, 19, 21, 2, 4, 339, DateTimeKind.Local).AddTicks(2046), null });

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "CreatedBy" },
                values: new object[] { new DateTime(2021, 8, 19, 21, 2, 4, 357, DateTimeKind.Local).AddTicks(7959), null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "CreatedBy" },
                values: new object[] { new DateTime(2021, 8, 19, 21, 2, 4, 330, DateTimeKind.Local).AddTicks(9261), null });
        }
    }
}
