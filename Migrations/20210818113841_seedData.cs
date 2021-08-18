using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NikeFarms.v2._0.Migrations
{
    public partial class seedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Name", "UpdatedAt" },
                values: new object[] { 1, new DateTime(2021, 8, 18, 12, 38, 39, 998, DateTimeKind.Local).AddTicks(4102), null, "SuperAdmin", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "CreatedAt", "CreatedBy", "Email", "FirstName", "HashSalt", "LastName", "PasswordHash", "PhoneNo", "UpdatedAt" },
                values: new object[] { 1, "lag", new DateTime(2021, 8, 18, 12, 38, 39, 995, DateTimeKind.Local).AddTicks(6186), null, "mazeedahhamzat@gmail.com", "Mazstar", "J5cdgq6p63lKwmVg3b7ltQ==", "Hamzy", "EGSROdXOgd5YsDofKkZatVGf2Cnc/7O/RxhqQSmOF30=", "09055220828", null });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "RoleId", "UpdatedAt", "UserId" },
                values: new object[] { 1, new DateTime(2021, 8, 18, 12, 38, 40, 6, DateTimeKind.Local).AddTicks(9270), null, 1, null, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
