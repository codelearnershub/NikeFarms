using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace NikeFarms.v2._0.Migrations
{
    public partial class addedMortalityClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mortality",
                table: "DailyActivities");

            migrationBuilder.CreateTable(
                name: "Mortalities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    NoOfDeaths = table.Column<int>(nullable: false),
                    StockId = table.Column<int>(nullable: true),
                    FlockId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mortalities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mortalities_Flocks_FlockId",
                        column: x => x.FlockId,
                        principalTable: "Flocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mortalities_Stocks_StockId",
                        column: x => x.StockId,
                        principalTable: "Stocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 8, 30, 15, 27, 36, 319, DateTimeKind.Local).AddTicks(6189));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 8, 30, 15, 27, 36, 328, DateTimeKind.Local).AddTicks(6131));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 8, 30, 15, 27, 36, 315, DateTimeKind.Local).AddTicks(7872));

            migrationBuilder.CreateIndex(
                name: "IX_Mortalities_FlockId",
                table: "Mortalities",
                column: "FlockId");

            migrationBuilder.CreateIndex(
                name: "IX_Mortalities_StockId",
                table: "Mortalities",
                column: "StockId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mortalities");

            migrationBuilder.AddColumn<int>(
                name: "Mortality",
                table: "DailyActivities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 8, 29, 20, 30, 19, 918, DateTimeKind.Local).AddTicks(3635));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 8, 29, 20, 30, 19, 925, DateTimeKind.Local).AddTicks(789));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 8, 29, 20, 30, 19, 915, DateTimeKind.Local).AddTicks(1179));
        }
    }
}
